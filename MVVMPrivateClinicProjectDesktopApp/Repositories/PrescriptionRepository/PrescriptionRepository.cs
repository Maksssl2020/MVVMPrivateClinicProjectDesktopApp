using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Base;
using MVVMPrivateClinicProjectDesktopApp.Repositories.DoctorRepository;
using MVVMPrivateClinicProjectDesktopApp.Repositories.PatientRepository;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.PrescriptionRepository;

public class PrescriptionRepository(
    DbContextFactory dbContextFactory,
    IMapper mapper,
    IPatientRepository patientRepository,
    IDoctorRepository doctorRepository
    ) : BaseRepository<Prescription,PrescriptionDto>(dbContextFactory, mapper), IPrescriptionRepository {
    private readonly DbContextFactory _dbContextFactory = dbContextFactory;
    private readonly IMapper _mapper = mapper;

    public async Task<PrescriptionDto> SavePrescriptionAsync(SavePrescriptionRequest prescriptionRequest){
        await using var context = _dbContextFactory.CreateDbContext();

        var dateIssued = DateOnly.FromDateTime(DateTime.Now);
        var expirationDate = prescriptionRequest.PrescriptionValidity.Equals(PrescriptionValidity.OneMonth)
            ? dateIssued.AddMonths(1)
            : dateIssued.AddYears(1);
        var prescriptionActivationCode = await GeneratePrescriptionActivationCode(context);

        Console.WriteLine(dateIssued);
        Console.WriteLine(expirationDate);
        
        var prescription = new Prescription {
            DateIssued = dateIssued,
            ExpirationDate = expirationDate,
            IdDoctor = prescriptionRequest.IdDoctor,
            IdPatient = prescriptionRequest.IdPatient,
            PrescriptionCode = prescriptionActivationCode,
            PrescriptionDescription = prescriptionRequest.PrescriptionDescription
        };

        foreach (var medicine in prescriptionRequest.SelectedMedicines) {
            var foundMedicine = await context.Medicines.FindAsync(medicine.Id);

            if (foundMedicine != null) {
                prescription.Medicines.Add(foundMedicine);
            }
        }
        
        context.Prescriptions.Add(prescription);
        await context.SaveChangesAsync();

        return _mapper.Map<PrescriptionDto>(prescription);
    }

    public async Task<PrescriptionDetailsDto> GetPrescriptionDetailsDtoByIdAsync(int prescriptionId){
        await using var context = _dbContextFactory.CreateDbContext();
        List<MedicineDto> medicinesDto = [];
        
        var foundPrescription = await context.Prescriptions
            .Include(m => m.Medicines)
            .FirstOrDefaultAsync(m => m.Id == prescriptionId);

        if (foundPrescription?.Medicines != null) {
            medicinesDto.AddRange(
                foundPrescription.Medicines
                    .Select(_mapper.Map<MedicineDto>)
                );
        }
            

        var prescriptionDto =  _mapper.Map<PrescriptionDetailsDto>(foundPrescription);
        var foundDoctor = await doctorRepository.GetDoctorDetailsAsync(prescriptionDto.IdDoctor);
        var foundPatient = await patientRepository.GetPatientDetailsAsync(prescriptionDto.IdPatient);
        
        prescriptionDto.MedicinesDto = medicinesDto;
        if (foundDoctor != null) prescriptionDto.DoctorDtoBase = foundDoctor;
        if (foundPatient != null) prescriptionDto.PatientDetailsDto = foundPatient;


        return prescriptionDto;
    }

    private static async Task<string> GeneratePrescriptionActivationCode(PrivateClinicContext context){
        var random = new Random();
        string generatedCode;

        do {
            generatedCode =  random.Next(100000, 999999).ToString();
        } while (!await IsPrescriptionActivationCodeValid(context, generatedCode));
        
        return generatedCode;
    }

    private static async Task<bool> IsPrescriptionActivationCodeValid(PrivateClinicContext context, string prescriptionActivationCode){
        var allCodes = await context.Prescriptions
            .Select(p => p.PrescriptionCode)
            .ToListAsync();
        
        return !allCodes.Contains(prescriptionActivationCode);
    }
    
    public async Task<IEnumerable<PrescriptionDto>> GetAllPrescriptionsDtoAsync(){
        var foundPrescriptionsDto = await GetAllEntitiesAsync();
        var allPrescriptionsDtoAsync = foundPrescriptionsDto.ToList();

        foreach (var prescriptionDto in allPrescriptionsDtoAsync) {
            await AppendPatientCodeAndDoctorCodeToPrescriptionDto(prescriptionDto);
        }
        
        return allPrescriptionsDtoAsync;
    }

    public async Task<IEnumerable<PrescriptionDto>> GetIssuedPrescriptionsByPatientIdOrDoctorId(int personId, PersonType personType){
        await using var context = _dbContextFactory.CreateDbContext();
        
        var foundPrescriptionsDto =  await context.Prescriptions
            .Where(prescription => personType.Equals(PersonType.Patient) ? prescription.IdPatient == personId : prescription.IdDoctor == personId)
            .ProjectTo<PrescriptionDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        foreach (var prescriptionDto in foundPrescriptionsDto) {
            await AppendPatientCodeAndDoctorCodeToPrescriptionDto(prescriptionDto);
        }
        
        return foundPrescriptionsDto;
    }

    public async Task<int> CountIssuedPrescriptionsAsync(){
        return await CountEntitiesAsync();
    }

    public async Task<int> CountIssuedPrescriptionsByDoctorIdAsync(int doctorId){
        await using var context = _dbContextFactory.CreateDbContext();
        return await context.Prescriptions
            .Where(prescription => prescription.IdDoctor == doctorId)
            .CountAsync();
    }

    public async Task<int> CountMedicineUsesAsync(int medicineId){
        await using var context = _dbContextFactory.CreateDbContext();
        
        return await context.Prescriptions
            .SelectMany(prescription => prescription.Medicines)
            .CountAsync(medicine  => medicine.Id == medicineId);
    }

    private async Task AppendPatientCodeAndDoctorCodeToPrescriptionDto(PrescriptionDto prescriptionDto){
        var foundPatient = await patientRepository.GetPatientByIdAsync(prescriptionDto.IdPatient);
        var foundDoctor = await doctorRepository.GetDoctorByIdAsync(prescriptionDto.IdDoctor);

        if (foundPatient?.PatientCode != null) prescriptionDto.PatientCode = foundPatient.PatientCode;
        if (foundDoctor?.DoctorCode != null) prescriptionDto.DoctorCode = foundDoctor.DoctorCode;
    }
}