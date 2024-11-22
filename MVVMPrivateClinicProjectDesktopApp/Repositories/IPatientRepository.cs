using MVVMPrivateClinicProjectDesktopApp.Entities;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories;

public interface IPatientRepository {
    IEnumerable<Patient> GetAllPatients();
}