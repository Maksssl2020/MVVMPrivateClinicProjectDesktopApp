using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

public interface IPatientRepository {
    Task<Models.Entities.Patient> SavePatientAsync(SavePatientRequest patient);
    Task<IEnumerable<Models.Entities.Patient>> GetAllPatientsAsync();
    Task<Models.Entities.Patient?> GetPatientByIdAsync(int id);
    void DeletePatient(int id);
}