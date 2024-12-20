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

    public async Task<PricingDto?> GetPricingByIdAsync(int pricingId){
        await using var context = dbContextFactory.CreateDbContext();
        
        return await context.Pricings
            .Where(p => p.Id == pricingId)
            .ProjectTo<PricingDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
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

    public async Task<IEnumerable<TopPricingDto>> GetTopPricingDtoAsync(int amount){
        await using var context = dbContextFactory.CreateDbContext();

        return await context.Pricings
            .Include(pricing => pricing.Appointments)
            .OrderByDescending(pricing => pricing.Appointments.Count)
            .Take(amount)
            .ProjectTo<TopPricingDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }
}