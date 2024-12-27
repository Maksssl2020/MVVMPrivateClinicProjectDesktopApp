using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Base;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.PricingRepository;

public interface IPricingRepository : IBaseRepository<PricingDto> {
    Task<PricingDto> SavePricingAsync(SavePricingRequest pricingRequest);
    Task<PricingDto?> GetPricingByIdAsync(int pricingId);
    Task<PricingDetailsDto?> GetPricingDetailsByIdAsync(int pricingId);
    Task<IEnumerable<PricingDto>> GetAllPricingDtoAsync();
    Task<IEnumerable<ServiceTypeDto>> GetAllServiceTypesDtoAsync();
    Task<IEnumerable<TopPricingDto>> GetTopPricingDtoAsync(int amount);
}