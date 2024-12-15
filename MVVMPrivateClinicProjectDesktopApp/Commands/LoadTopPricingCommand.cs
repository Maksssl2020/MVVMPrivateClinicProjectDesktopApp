using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadTopPricingCommand(HomeViewModel viewModel, PricingStore pricingStore): AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await pricingStore.LoadTopPricingAsync();
            viewModel.UpdateTopPricing(pricingStore.TopPricingDto);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}