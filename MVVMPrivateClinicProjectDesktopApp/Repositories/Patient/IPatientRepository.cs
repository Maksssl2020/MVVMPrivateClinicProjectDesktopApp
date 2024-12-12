using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

public interface IPatientRepository {
    Task<PatientDto> SavePatientAsync(SavePatientRequest patient);
    Task<IEnumerable<PatientDto>> GetAllPatientsAsync();
    Task<PatientDto?> GetPatientByIdAsync(int patientId);
    Task<PatientDetailsDto?> GetPatientDetailsAsync(int patientId);
    void DeletePatient(int patientId);
}