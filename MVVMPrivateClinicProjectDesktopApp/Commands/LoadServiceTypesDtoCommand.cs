using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadServiceTypesDtoCommand(AddNewPricingViewModel viewModel, PricingStore pricingStore): AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await pricingStore.LoadServiceTypesAsync();
            viewModel.UpdateServiceTypes(pricingStore.ServiceTypesDto);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}