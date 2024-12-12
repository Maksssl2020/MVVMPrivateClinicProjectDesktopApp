using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadPricingDtoCommand(IPricingViewModel viewModel, PricingStore pricingStore): AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await pricingStore.LoadPricingAsync();
            viewModel.UpdatePricing(pricingStore.PricingDto);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}