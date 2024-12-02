using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.DoctorSpecialization;

public class DoctorSpecializationRepository(DbContextFactory dbContextFactory) : IDoctorSpecializationRepository {
    public async Task<IEnumerable<Models.Entities.DoctorSpecialization>> GetAllDoctorSpecializations(){
        await using var context = dbContextFactory.CreateDbContext();
        return await context.DoctorSpecializations.ToListAsync();
    }

    public async Task<Models.Entities.DoctorSpecialization?> GetDoctorSpecializationById(int id){
        await using var context = dbContextFactory.CreateDbContext();
        return await context.DoctorSpecializations
            .FirstOrDefaultAsync(doctorSpecialization => doctorSpecialization.Id == id)!;
    }
}