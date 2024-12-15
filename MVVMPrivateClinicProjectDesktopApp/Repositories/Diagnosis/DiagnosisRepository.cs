using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Disease;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Doctor;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Diagnosis;

public class DiagnosisRepository(
    DbContextFactory dbContextFactory,
    IMapper mapper,
    IPatientRepository patientRepository,
    IDoctorRepository doctorRepository,
    IDiseaseRepository diseaseRepository
    ) : IDiagnosisRepository {
    public async Task<DiagnosisDto> SaveDiagnosisAsync(SaveDiagnosisRequest diagnosisRequest){
        await using var context = dbContextFactory.CreateDbContext();
        
        var diagnosisDate = DateOnly.FromDateTime(DateTime.Now);

        Console.WriteLine($"Saving diagnosis: {diagnosisDate}");
        
        var diagnosis = new Models.Entities.Diagnosis {
            DiagnosisDate = diagnosisDate,
            Description = diagnosisRequest.Description,
            IdPatient = diagnosisRequest.PatientId,
            IdDoctor = diagnosisRequest.DoctorId,
            IdDisease = diagnosisRequest.DiseaseId
        };
        
        await context.Diagnoses.AddAsync(diagnosis);
        await context.SaveChangesAsync();

        var diagnosisDto = mapper.Map<DiagnosisDto>(diagnosis);

        await AppendPatientCodeDoctorCodeAndDiseaseCodeToDiagnosisDto(diagnosis, diagnosisDto);
        return diagnosisDto;
    }

    public async Task<IEnumerable<DiagnosisDto>> GetPatientAllDiagnosisAsync(int patientId){
        await using var context = dbContextFactory.CreateDbContext();

        List<DiagnosisDto> diagnosesDto = [];
        
        var diagnoses = await context.Diagnoses
            .Where(diagnosis => diagnosis.IdPatient == patientId)
            .ToListAsync();

        foreach (var diagnosis in diagnoses) {
            var diagnosisDto = mapper.Map<DiagnosisDto>(diagnosis);
            await AppendPatientCodeDoctorCodeAndDiseaseCodeToDiagnosisDto(diagnosis, diagnosisDto);
            diagnosesDto.Add(diagnosisDto);
        }
        
        return diagnosesDto;
    }

    public async Task<IEnumerable<DiagnosisDto>> GetAllDiagnosisAsync(){
        await using var context = dbContextFactory.CreateDbContext();
        List<DiagnosisDto> diagnosesDto = [];
        
        var diagnoses = await context.Diagnoses
            .ToListAsync();
        
        foreach (var diagnosis in diagnoses) {
            var diagnosisDto = mapper.Map<DiagnosisDto>(diagnosis);
            await AppendPatientCodeDoctorCodeAndDiseaseCodeToDiagnosisDto(diagnosis, diagnosisDto);
            diagnosesDto.Add(diagnosisDto);
        }
        
        return diagnosesDto;
    }

    public async Task<int> CountIssuedDiagnosisAsync(){
        await using var context = dbContextFactory.CreateDbContext();
        return await context.Diagnoses.CountAsync();
    }

    private async Task AppendPatientCodeDoctorCodeAndDiseaseCodeToDiagnosisDto(Models.Entities.Diagnosis diagnosis, DiagnosisDto diagnosisDto){
        var foundPatient = await patientRepository.GetPatientByIdAsync(diagnosis.IdPatient);
        var foundDoctor = await doctorRepository.GetDoctorByIdAsync(diagnosis.IdDoctor);
        var foundDisease = await diseaseRepository.GetDiseaseByIdAsync(diagnosis.IdDisease);

        if (foundPatient?.PatientCode != null) diagnosisDto.PatientCode = foundPatient.PatientCode;
        if (foundDoctor?.DoctorCode != null) diagnosisDto.DoctorCode = foundDoctor.DoctorCode;
        if (foundDisease != null) diagnosisDto.DiseaseCode = foundDisease.DiseaseCode;
    }
}