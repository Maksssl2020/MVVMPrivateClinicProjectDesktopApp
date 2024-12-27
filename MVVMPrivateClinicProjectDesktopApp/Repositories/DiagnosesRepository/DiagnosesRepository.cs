using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Base;
using MVVMPrivateClinicProjectDesktopApp.Repositories.DiseaseRepository;
using MVVMPrivateClinicProjectDesktopApp.Repositories.DoctorRepository;
using MVVMPrivateClinicProjectDesktopApp.Repositories.PatientRepository;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.DiagnosesRepository;

public class DiagnosesRepository(
    DbContextFactory dbContextFactory,
    IMapper mapper,
    IPatientRepository patientRepository,
    IDoctorRepository doctorRepository,
    IDiseaseRepository diseaseRepository
    ) : BaseRepository<Diagnosis, DiagnosisDto>(dbContextFactory, mapper), IDiagnosesRepository {
    private readonly DbContextFactory _dbContextFactory = dbContextFactory;
    private readonly IMapper _mapper = mapper;

    public async Task<DiagnosisDto> SaveDiagnosisAsync(SaveDiagnosisRequest diagnosisRequest){
        await using var context = _dbContextFactory.CreateDbContext();
        
        var diagnosisDate = DateOnly.FromDateTime(DateTime.Now);

        Console.WriteLine($"Saving diagnosis: {diagnosisDate}");
        
        var diagnosis = new Diagnosis {
            DiagnosisDate = diagnosisDate,
            Description = diagnosisRequest.Description,
            IdPatient = diagnosisRequest.PatientId,
            IdDoctor = diagnosisRequest.DoctorId,
            IdDisease = diagnosisRequest.DiseaseId
        };
        
        await context.Diagnoses.AddAsync(diagnosis);
        await context.SaveChangesAsync();

        var diagnosisDto = _mapper.Map<DiagnosisDto>(diagnosis);

        await AppendPatientCodeDoctorCodeAndDiseaseCodeToDiagnosisDto(diagnosisDto);
        return diagnosisDto;
    }

    public async Task<IEnumerable<DiagnosisDto>> GetIssuedDiagnosesByPatientIdOrDoctorId(int personId, PersonType personType){
        await using var context = _dbContextFactory.CreateDbContext();

        List<DiagnosisDto> diagnosesDto = [];
        
        var diagnoses = await context.Diagnoses
            .Where(diagnosis => personType.Equals(PersonType.Patient) ? diagnosis.IdPatient == personId : diagnosis.IdDoctor == personId)
            .ToListAsync();

        foreach (var diagnosisDto in diagnoses.Select(diagnosis => _mapper.Map<DiagnosisDto>(diagnosis))) {
            await AppendPatientCodeDoctorCodeAndDiseaseCodeToDiagnosisDto(diagnosisDto);
            diagnosesDto.Add(diagnosisDto);
        }
        
        return diagnosesDto;
    }

    public async Task<IEnumerable<DiagnosisDto>> GetAllDiagnosesAsync(){
        await using var context = _dbContextFactory.CreateDbContext();
        var diagnosesDto = await GetAllEntitiesAsync();
        var allDiagnosesAsync = diagnosesDto.ToList();
        
        foreach (var diagnosisDto in allDiagnosesAsync) {
            await AppendPatientCodeDoctorCodeAndDiseaseCodeToDiagnosisDto(diagnosisDto);
        }
        
        return allDiagnosesAsync;
    }

    public async Task<int> CountIssuedDiagnosisAsync(){
        return await CountEntitiesAsync();
    }

    public async Task<int> CountIssuedDiagnosisByDoctorIdAsync(int doctorId){
        await using var context = _dbContextFactory.CreateDbContext();
        return await context.Diagnoses
            .Where(diagnosis => diagnosis.IdDoctor == doctorId)
            .CountAsync();
    }

    public async Task<int> CountDiagnosedDiseaseAsync(int diseaseId){
        await using var context = _dbContextFactory.CreateDbContext();
        return await context.Diagnoses
            .Where(diagnosis => diagnosis.IdDisease == diseaseId)
            .CountAsync();
    }

    private async Task AppendPatientCodeDoctorCodeAndDiseaseCodeToDiagnosisDto(DiagnosisDto diagnosisDto){
        var foundPatient = await patientRepository.GetPatientByIdAsync(diagnosisDto.IdPatient);
        var foundDoctor = await doctorRepository.GetDoctorByIdAsync(diagnosisDto.IdDoctor);
        var foundDisease = await diseaseRepository.GetDiseaseByIdAsync(diagnosisDto.IdDisease);

        if (foundPatient?.PatientCode != null) diagnosisDto.PatientCode = foundPatient.PatientCode;
        if (foundDoctor?.DoctorCode != null) diagnosisDto.DoctorCode = foundDoctor.DoctorCode;
        if (foundDisease != null) diagnosisDto.DiseaseCode = foundDisease.DiseaseCode;
    }
}