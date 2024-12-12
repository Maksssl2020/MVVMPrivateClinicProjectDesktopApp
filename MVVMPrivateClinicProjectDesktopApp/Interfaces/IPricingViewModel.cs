using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Interfaces;

public interface IPricingViewModel {
    public void UpdatePricing(IEnumerable<PricingDto> pricingDto);
}