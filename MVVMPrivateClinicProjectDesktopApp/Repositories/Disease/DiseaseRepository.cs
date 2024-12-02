using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Disease;

public class DiseaseRepository(DbContextFactory dbContextFactory) : IDiseaseRepository {
    public async Task<IEnumerable<Models.Entities.Disease>> GetAllDiseasesAsync(){
        await using var context = dbContextFactory.CreateDbContext();
        return await context.Diseases.ToListAsync();
    }
}