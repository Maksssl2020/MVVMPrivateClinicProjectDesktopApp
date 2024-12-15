using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

public class PatientRepository(DbContextFactory dbContextFactory, IMapper mapper) : IPatientRepository {
    public async Task<PatientDto> SavePatientAsync(SavePatientRequest patient){
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
        
        return mapper.Map<PatientDto>(createdPatient);
    }

    public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync(){
        await using var context = dbContextFactory.CreateDbContext();
        return await context.Patients
            .ProjectTo<PatientDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<PatientDto?> GetPatientByIdAsync(int id){
        await using var context = dbContextFactory.CreateDbContext();
        return await context.Patients
            .ProjectTo<PatientDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(patient => patient.Id == id);
    }

    public async Task<PatientDetailsDto?> GetPatientDetailsAsync(int patientId){
        await using var context = dbContextFactory.CreateDbContext();
        
        return await context.Patients
            .ProjectTo<PatientDetailsDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(patient => patient.Id == patientId);
    }

    public void DeletePatient(int id){
        using var context = dbContextFactory.CreateDbContext();
        context.Patients
            .Where(patient => patient.Id == id)
            .ExecuteDelete();
    }

    public async Task<int> CountPatientsAsync(){
        await using var context = dbContextFactory.CreateDbContext();
        return await context.Patients.CountAsync();
    }
}