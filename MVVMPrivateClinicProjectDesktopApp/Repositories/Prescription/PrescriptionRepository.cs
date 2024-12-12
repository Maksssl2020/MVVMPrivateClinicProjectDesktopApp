using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Doctor;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Medicine;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Prescription;

public class PrescriptionRepository(
    DbContextFactory dbContextFactory,
    IMapper mapper,
    IPatientRepository patientRepository,
    IDoctorRepository doctorRepository
    ) : IPrescriptionRepository {
    public async Task<PrescriptionDto> SavePrescriptionAsync(SavePrescriptionRequest prescriptionRequest){
        await using var context = dbContextFactory.CreateDbContext();

        var dateIssued = DateOnly.FromDateTime(DateTime.Now);
        var expirationDate = prescriptionRequest.PrescriptionValidity.Equals(PrescriptionValidity.OneMonth)
            ? dateIssued.AddMonths(1)
            : dateIssued.AddYears(1);
        var prescriptionActivationCode = await GeneratePrescriptionActivationCode(context);

        Console.WriteLine(dateIssued);
        Console.WriteLine(expirationDate);
        
        var prescription = new Models.Entities.Prescription {
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

        return mapper.Map<PrescriptionDto>(prescription);
    }

    public async Task<PrescriptionDetailsDto> GetPrescriptionDetailsDtoByIdAsync(int prescriptionId){
        await using var context = dbContextFactory.CreateDbContext();
        List<MedicineDto> medicinesDto = [];
        
        var foundPrescription = await context.Prescriptions
            .Include(m => m.Medicines)
            .FirstOrDefaultAsync(m => m.Id == prescriptionId);

        if (foundPrescription?.Medicines != null) {
            medicinesDto.AddRange(
                foundPrescription.Medicines
                    .Select(mapper.Map<MedicineDto>)
                );
        }
            

        var prescriptionDto =  mapper.Map<PrescriptionDetailsDto>(foundPrescription);
        var foundDoctor = await doctorRepository.GetDoctorFullNameAndSpecializationDtoByIdAsync(prescriptionDto.IdDoctor);
        var foundPatient = await patientRepository.GetPatientFullNameDtoByIdAsync(prescriptionDto.IdPatient);
        
        prescriptionDto.MedicinesDto = medicinesDto;
        if (foundDoctor != null) prescriptionDto.DoctorDetailsDto = foundDoctor;
        if (foundPatient != null) prescriptionDto.PatientDetailsCto = foundPatient;


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
        await using var context = dbContextFactory.CreateDbContext();

        var foundPrescriptionsDto = await context.Prescriptions
            .ProjectTo<PrescriptionDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        foreach (var prescriptionDto in foundPrescriptionsDto) {
            await AppendPatientCodeAndDoctorCodeToPrescriptionDto(prescriptionDto);
        }
        
        return foundPrescriptionsDto;
    }

    public async Task<IEnumerable<PrescriptionDto>> GetPatientAllPrescriptionsDtoAsync(int patientId){
        await using var context = dbContextFactory.CreateDbContext();
        
        var foundPrescriptionsDto =  await context.Prescriptions
            .Where(prescription => prescription.IdPatient == patientId)
            .ProjectTo<PrescriptionDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        foreach (var prescriptionDto in foundPrescriptionsDto) {
            await AppendPatientCodeAndDoctorCodeToPrescriptionDto(prescriptionDto);
        }
        
        return foundPrescriptionsDto;
    }

    private async Task AppendPatientCodeAndDoctorCodeToPrescriptionDto(PrescriptionDto prescriptionDto){
        var foundPatient = await patientRepository.GetPatientByIdAsync(prescriptionDto.IdPatient);
        var foundDoctor = await doctorRepository.GetDoctorByIdAsync(prescriptionDto.IdDoctor);

        if (foundPatient?.PatientCode != null) prescriptionDto.PatientCode = foundPatient.PatientCode;
        if (foundDoctor?.DoctorCode != null) prescriptionDto.DoctorCode = foundDoctor.DoctorCode;
    }
}