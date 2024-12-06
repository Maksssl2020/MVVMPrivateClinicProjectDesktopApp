using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Pricing;

public class PricingRepository(DbContextFactory dbContextFactory, IMapper mapper) : IPricingRepository {
    public async Task<IEnumerable<PricingDto>> GetAllPricingDtoAsync(){
        await using var context = dbContextFactory.CreateDbContext();

        return await context.Pricings
            .ProjectTo<PricingDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }
}