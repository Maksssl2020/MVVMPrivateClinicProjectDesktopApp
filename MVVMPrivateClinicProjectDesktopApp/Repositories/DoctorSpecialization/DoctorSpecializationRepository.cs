using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.DoctorSpecialization;

public class DoctorSpecializationRepository(DbContextFactory dbContextFactory, IMapper mapper) : IDoctorSpecializationRepository {
    public async Task<DoctorSpecializationDto> SaveDoctorSpecializationAsync(string doctorSpecializationName){
        await using var context = dbContextFactory.CreateDbContext();

        var doctorSpecialization = new Models.Entities.DoctorSpecialization {
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

    public async Task<DoctorSpecializationDto?> GetDoctorSpecializationById(int id){
        await using var context = dbContextFactory.CreateDbContext();
        return await context.DoctorSpecializations
            .ProjectTo<DoctorSpecializationDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(doctorSpecialization => doctorSpecialization.Id == id)!;
    }

    public async Task<bool> IsDoctorSpecializationExist(string doctorSpecializationName){
        await using var context = dbContextFactory.CreateDbContext();
        Console.WriteLine(doctorSpecializationName);
        
        return await context.DoctorSpecializations
            .AnyAsync(doctorSpecialization => doctorSpecialization.Name.ToUpper() == doctorSpecializationName.ToUpper());
    }

    public async Task<int?> GetDoctorSpecializationId(string doctorSpecializationName){
        await using var context = dbContextFactory.CreateDbContext();
        Console.WriteLine(doctorSpecializationName);
        
        
        var foundSpecialization = await context.DoctorSpecializations
            .Where(doctorSpecialization => doctorSpecialization.Name.ToUpper() == doctorSpecializationName.ToUpper())
            .FirstOrDefaultAsync();
        
        return foundSpecialization?.Id;
    }
}