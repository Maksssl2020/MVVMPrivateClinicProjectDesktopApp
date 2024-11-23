namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

public interface IPatientRepository {
    IEnumerable<Models.Entities.Patient> GetAllPatients();
    Models.Entities.Patient? GetPatientById(int id);
}