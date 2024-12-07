using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Pricing;

public class PricingRepository(DbContextFactory dbContextFactory, IMapper mapper) : IPricingRepository {
    public async Task<PricingDto> SavePricingAsync(SavePricingRequest pricingRequest){
        await using var context = dbContextFactory.CreateDbContext();

        var pricing = new Models.Entities.Pricing {
            ServiceName = pricingRequest.ServiceName,
            ServiceType = pricingRequest.ServiceType,
            Price = pricingRequest.Price,
            EffectiveDate = DateOnly.FromDateTime(DateTime.Now),
            IsAvailable = true
        };
        
        await context.Pricings.AddAsync(pricing);
        await context.SaveChangesAsync();
        
        return mapper.Map<PricingDto>(pricing);
    }

    public async Task<IEnumerable<PricingDto>> GetAllPricingDtoAsync(){
        await using var context = dbContextFactory.CreateDbContext();

        return await context.Pricings
            .ProjectTo<PricingDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<ServiceTypeDto>> GetAllServiceTypesDtoAsync(){
        await using var context = dbContextFactory.CreateDbContext();

        return await context.Pricings
            .Select(pricing => pricing.ServiceType)
            .Distinct()
            .Select(type => new ServiceTypeDto {Type = type})
            .ToListAsync();
    }
}