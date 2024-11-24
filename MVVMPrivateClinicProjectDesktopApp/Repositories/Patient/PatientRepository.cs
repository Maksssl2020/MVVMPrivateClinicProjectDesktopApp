using Microsoft.EntityFrameworkCore;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

public class PatientRepository : RepositoryBase, IPatientRepository {
    public async Task<IEnumerable<Models.Entities.Patient>> GetAllPatientsAsync(){
        return await DbContext.Patients.ToListAsync();
    }

    public async Task<Models.Entities.Patient?> GetPatientById(int id){
        return await DbContext.Patients
            .FirstOrDefaultAsync(patient => patient.Id == id);
    }
}