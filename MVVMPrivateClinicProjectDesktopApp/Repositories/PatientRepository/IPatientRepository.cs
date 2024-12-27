using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Base;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.PatientRepository;

public interface IPatientRepository : IBaseRepository<PatientDto> {
    Task<PatientDto> SavePatientAsync(SavePatientRequest patient);
    Task<IEnumerable<PatientDto>> GetAllPatientsAsync();
    Task<PatientDto?> GetPatientByIdAsync(int patientId);
    Task<PatientDetailsDto?> GetPatientDetailsAsync(int patientId);
    Task<int> CountPatientsAsync();
}