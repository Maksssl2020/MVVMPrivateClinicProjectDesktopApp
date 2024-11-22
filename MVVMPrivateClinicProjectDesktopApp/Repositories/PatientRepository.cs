using MVVMPrivateClinicProjectDesktopApp.Entities;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories;

public class PatientRepository : RepositoryBase, IPatientRepository {
    public IEnumerable<Patient> GetAllPatients(){
        return DbContext.Patients.ToList();
    }
}