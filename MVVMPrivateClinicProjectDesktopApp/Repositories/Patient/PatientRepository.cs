using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

public class PatientRepository : RepositoryBase, IPatientRepository {
    public async Task<Models.Entities.Patient> SavePatientAsync(SavePatientRequest patient){
        var createdPatient = new Models.Entities.Patient {
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            PhoneNumber = patient.PhoneNumber,
            EmailAddress = patient.Email,
            IdAddress = patient.AddressId
        };
        
        await DbContext.Patients.AddAsync(createdPatient);
        await DbContext.SaveChangesAsync();
        
        return createdPatient;
    }

    public async Task<IEnumerable<Models.Entities.Patient>> GetAllPatientsAsync(){
        return await DbContext.Patients.ToListAsync();
    }

    public async Task<Models.Entities.Patient?> GetPatientById(int id){
        return await DbContext.Patients
            .FirstOrDefaultAsync(patient => patient.Id == id);
    }

    public void DeletePatient(int id){
        DbContext.Patients
            .Where(patient => patient.Id == id)
            .ExecuteDelete();
    }
}