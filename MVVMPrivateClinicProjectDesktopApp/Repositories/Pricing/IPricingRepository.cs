using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Pricing;

public interface IPricingRepository {
    Task<PricingDto> SavePricingAsync(SavePricingRequest pricingRequest);
    Task<PricingDto?> GetPricingByIdAsync(int pricingId);
    Task<IEnumerable<PricingDto>> GetAllPricingDtoAsync();
    Task<IEnumerable<ServiceTypeDto>> GetAllServiceTypesDtoAsync();
}