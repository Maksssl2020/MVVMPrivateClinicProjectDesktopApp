using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Pricing;

public interface IPricingRepository {
    Task<IEnumerable<PricingDto>> GetAllPricingDtoAsync();
}