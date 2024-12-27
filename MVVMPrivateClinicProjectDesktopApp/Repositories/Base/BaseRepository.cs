using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Base;

public abstract class BaseRepository<T, TV>(DbContextFactory dbContextFactory, IMapper mapper) : IBaseRepository<TV> where T : class, IDeletableEntity where TV : class {
    public async Task<IEnumerable<TV>> GetAllEntitiesAsync(){
        await using var context = dbContextFactory.CreateDbContext();
        return await context.Set<T>()
            .Where(e => e.IsDeleted == false)
            .ProjectTo<TV>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<TV?> GetEntityByIdAsync(int entityId){
        await using var context = dbContextFactory.CreateDbContext();
        return await context.Set<T>()
            .Where(e => e.Id == entityId)
            .ProjectTo<TV>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }
    
    public virtual async Task DeleteEntityAsync(int entityId){
        await using var context = dbContextFactory.CreateDbContext();
        
        var foundEntity = await context.Set<T>().FindAsync(entityId);

        if (foundEntity is IDeletableEntity entity) {
            entity.IsDeleted = true;
            entity.DisabledDate = DateTime.Now;
            
            await context.SaveChangesAsync();
        }
    }

    public async Task<int> CountEntitiesAsync(){
        await using var context = dbContextFactory.CreateDbContext();
        return await context.Set<T>().Where(e => e.IsDeleted == false).CountAsync();
    }
}