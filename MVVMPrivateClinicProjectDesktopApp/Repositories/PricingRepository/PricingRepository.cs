using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Base;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.PricingRepository;

public class PricingRepository(DbContextFactory dbContextFactory, IMapper mapper)
    : BaseRepository<Pricing, PricingDto>(dbContextFactory, mapper), IPricingRepository {
    private readonly DbContextFactory _dbContextFactory = dbContextFactory;
    private readonly IMapper _mapper = mapper;

    public async Task<PricingDto> SavePricingAsync(SavePricingRequest pricingRequest){
        await using var context = _dbContextFactory.CreateDbContext();

        var pricing = new Pricing {
            ServiceName = pricingRequest.ServiceName,
            ServiceType = pricingRequest.ServiceType,
            Price = pricingRequest.Price,
            EffectiveDate = DateOnly.FromDateTime(DateTime.Now),
            IsDeleted = false
        };
        
        await context.Pricings.AddAsync(pricing);
        await context.SaveChangesAsync();
        
        return _mapper.Map<PricingDto>(pricing);
    }

    public async Task<PricingDto?> GetPricingByIdAsync(int pricingId){
        return await GetEntityByIdAsync(pricingId);
    }

    public async Task<PricingDetailsDto?> GetPricingDetailsByIdAsync(int pricingId){
        await using var context = _dbContextFactory.CreateDbContext();
        
        return await context.Pricings
            .Where(p => p.Id == pricingId)
            .ProjectTo<PricingDetailsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<PricingDto>> GetAllPricingDtoAsync(){
        return await GetAllEntitiesAsync();
    }

    public async Task<IEnumerable<ServiceTypeDto>> GetAllServiceTypesDtoAsync(){
        await using var context = _dbContextFactory.CreateDbContext();

        return await context.Pricings
            .Select(pricing => pricing.ServiceType)
            .Distinct()
            .Select(type => new ServiceTypeDto {Type = type})
            .ToListAsync();
    }

    public async Task<IEnumerable<TopPricingDto>> GetTopPricingDtoAsync(int amount){
        await using var context = _dbContextFactory.CreateDbContext();

        return await context.Pricings
            .Include(pricing => pricing.Appointments)
            .OrderByDescending(pricing => pricing.Appointments.Count)
            .Take(amount)
            .ProjectTo<TopPricingDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}