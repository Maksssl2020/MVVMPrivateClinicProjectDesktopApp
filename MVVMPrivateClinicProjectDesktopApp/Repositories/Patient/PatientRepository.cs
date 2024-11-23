namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

public class PatientRepository : RepositoryBase, IPatientRepository {
    public IEnumerable<Models.Entities.Patient> GetAllPatients(){
        return DbContext.Patients.ToList();
    }

    public Models.Entities.Patient? GetPatientById(int id){
        return DbContext.Patients
            .FirstOrDefault(patient => patient.Id == id);
    }
}