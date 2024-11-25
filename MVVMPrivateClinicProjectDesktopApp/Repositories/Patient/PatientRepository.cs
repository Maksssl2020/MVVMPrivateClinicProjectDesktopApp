using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

public class PatientRepository : RepositoryBase, IPatientRepository {
    public void SavePatient(SavePatientRequest patient){
        var createdPatient = new Models.Entities.Patient {
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            PhoneNumber = patient.PhoneNumber,
            EmailAddress = patient.Email,
            IdAddress = patient.AddressId
        };
        
        DbContext.Patients.Add(createdPatient);
        DbContext.SaveChanges();
    }

    public async Task<IEnumerable<Models.Entities.Patient>> GetAllPatientsAsync(){
        return await DbContext.Patients.ToListAsync();
    }

    public async Task<Models.Entities.Patient?> GetPatientById(int id){
        return await DbContext.Patients
            .FirstOrDefaultAsync(patient => patient.Id == id);
    }
}