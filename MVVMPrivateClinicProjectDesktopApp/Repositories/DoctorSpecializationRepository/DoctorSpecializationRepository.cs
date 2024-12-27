using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.DoctorSpecializationRepository;

public class DoctorSpecializationRepository(DbContextFactory dbContextFactory, IMapper mapper) : IDoctorSpecializationRepository {
    public async Task<DoctorSpecializationDto> SaveDoctorSpecializationAsync(string doctorSpecializationName){
        await using var context = dbContextFactory.CreateDbContext();

        var doctorSpecialization = new DoctorSpecialization {
            Name = doctorSpecializationName
        };
        
        await context.DoctorSpecializations.AddAsync(doctorSpecialization);
        await context.SaveChangesAsync();
        
        return mapper.Map<DoctorSpecializationDto>(doctorSpecialization);
    }

    public async Task<IEnumerable<DoctorSpecializationDto>> GetAllDoctorSpecializations(){
        await using var context = dbContextFactory.CreateDbContext();
        return await context.DoctorSpecializations
            .ProjectTo<DoctorSpecializationDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<bool> IsDoctorSpecializationExists(string doctorSpecializationName){
        await using var context = dbContextFactory.CreateDbContext();
        
        return await context.DoctorSpecializations
            .AnyAsync(doctorSpecialization => doctorSpecialization.Name.Equals(doctorSpecializationName, StringComparison.CurrentCultureIgnoreCase));
    }

    public async Task<int?> GetDoctorSpecializationId(string doctorSpecializationName){
        await using var context = dbContextFactory.CreateDbContext();
        
        var foundSpecialization = await context.DoctorSpecializations
            .Where(doctorSpecialization => doctorSpecialization.Name.Equals(doctorSpecializationName, StringComparison.CurrentCultureIgnoreCase))
            .FirstOrDefaultAsync();
        
        return foundSpecialization?.Id;
    }
}