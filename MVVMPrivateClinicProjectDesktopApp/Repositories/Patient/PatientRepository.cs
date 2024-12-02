using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

public class PatientRepository(DbContextFactory dbContextFactory) : IPatientRepository {
    public async Task<Models.Entities.Patient> SavePatientAsync(SavePatientRequest patient){
        await using var context = dbContextFactory.CreateDbContext();
        var createdPatient = new Models.Entities.Patient {
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            PhoneNumber = patient.PhoneNumber,
            EmailAddress = patient.Email,
            IdAddress = patient.AddressId
        };
        
        await context.Patients.AddAsync(createdPatient);
        await context.SaveChangesAsync();
        
        return createdPatient;
    }

    public async Task<IEnumerable<Models.Entities.Patient>> GetAllPatientsAsync(){
        await using var context = dbContextFactory.CreateDbContext();
        return await context.Patients.ToListAsync();
    }

    public async Task<Models.Entities.Patient?> GetPatientByIdAsync(int id){
        await using var context = dbContextFactory.CreateDbContext();
        return await context.Patients
            .FirstOrDefaultAsync(patient => patient.Id == id);
    }

    public void DeletePatient(int id){
        using var context = dbContextFactory.CreateDbContext();
        context.Patients
            .Where(patient => patient.Id == id)
            .ExecuteDelete();
    }
}