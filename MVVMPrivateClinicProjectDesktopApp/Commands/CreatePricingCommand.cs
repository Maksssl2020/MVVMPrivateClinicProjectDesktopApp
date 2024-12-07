using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class CreatePricingCommand(AddNewPricingViewModel viewModel, PricingStore pricingStore, Action resetForm) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        var savePricingRequest = new SavePricingRequest {
            ServiceName = viewModel.ServiceName,
            Price = viewModel.Price,
            ServiceType = viewModel.ServiceType.Type
        };
        
        await pricingStore.CreatePricingAsync(savePricingRequest);
        resetForm.Invoke();
    }
}