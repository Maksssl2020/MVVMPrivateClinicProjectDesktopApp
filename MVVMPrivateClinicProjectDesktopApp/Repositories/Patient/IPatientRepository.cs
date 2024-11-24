namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

public interface IPatientRepository {
    Task<IEnumerable<Models.Entities.Patient>> GetAllPatientsAsync();
    Task<Models.Entities.Patient?> GetPatientById(int id);
}