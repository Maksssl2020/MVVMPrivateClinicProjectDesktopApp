using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Base;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.DiagnosesRepository;

public interface IDiagnosesRepository : IBaseRepository<DiagnosisDto> {
    Task<DiagnosisDto> SaveDiagnosisAsync(SaveDiagnosisRequest diagnosisRequest);
    Task<IEnumerable<DiagnosisDto>> GetIssuedDiagnosesByPatientIdOrDoctorId(int personId, PersonType personType);
    Task<IEnumerable<DiagnosisDto>> GetAllDiagnosesAsync();
    Task<int> CountIssuedDiagnosisAsync();
    Task<int> CountIssuedDiagnosisByDoctorIdAsync(int doctorId);
    Task<int> CountDiagnosedDiseaseAsync(int diseaseId);
}