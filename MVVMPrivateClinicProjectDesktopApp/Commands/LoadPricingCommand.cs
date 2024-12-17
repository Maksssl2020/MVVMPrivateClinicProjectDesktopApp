using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadPricingCommand(Action<IEnumerable<PricingDto>> updatePricing, PricingStore pricingStore): AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await pricingStore.LoadPricingAsync();
            updatePricing.Invoke(pricingStore.PricingDto);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}