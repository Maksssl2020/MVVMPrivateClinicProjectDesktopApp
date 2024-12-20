using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Diagnosis;

public interface IDiagnosisRepository {
    Task<DiagnosisDto> SaveDiagnosisAsync(SaveDiagnosisRequest diagnosisRequest);
    Task<IEnumerable<DiagnosisDto>> GetPatientAllDiagnosisAsync(int patientId);
    Task<IEnumerable<DiagnosisDto>> GetAllDiagnosisAsync();
    Task<int> CountIssuedDiagnosisAsync();
    Task<int> CountIssuedDiagnosisByDoctorIdAsync(int doctorId);
}