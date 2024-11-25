using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

public interface IPatientRepository {
    void SavePatient(SavePatientRequest patient);
    Task<IEnumerable<Models.Entities.Patient>> GetAllPatientsAsync();
    Task<Models.Entities.Patient?> GetPatientById(int id);
}