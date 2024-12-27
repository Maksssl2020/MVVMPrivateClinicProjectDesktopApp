using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Base;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.PatientRepository;

public class PatientRepository(DbContextFactory dbContextFactory, IMapper mapper)
    : BaseRepository<Patient, PatientDto>(dbContextFactory, mapper), IPatientRepository {
    private readonly DbContextFactory _dbContextFactory = dbContextFactory;
    private readonly IMapper _mapper = mapper;

    public async Task<PatientDto> SavePatientAsync(SavePatientRequest patient){
        await using var context = _dbContextFactory.CreateDbContext();
        var createdPatient = new Patient {
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            PhoneNumber = patient.PhoneNumber,
            EmailAddress = patient.Email,
            IdAddress = patient.AddressId
        };
        
        await context.Patients.AddAsync(createdPatient);
        await context.SaveChangesAsync();
        
        return _mapper.Map<PatientDto>(createdPatient);
    }

    public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync(){
        return await GetAllEntitiesAsync();
    }

    public async Task<PatientDto?> GetPatientByIdAsync(int patientId){
        return await GetEntityByIdAsync(patientId);
    }

    public async Task<PatientDetailsDto?> GetPatientDetailsAsync(int patientId){
        await using var context = _dbContextFactory.CreateDbContext();
        
        return await context.Patients
            .ProjectTo<PatientDetailsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(patient => patient.Id == patientId);
    }

    public async Task<int> CountPatientsAsync(){
        return await CountEntitiesAsync();
    }
}