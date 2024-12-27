using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PricingDetailsViewModel(PricingStore pricingStore)
    : EntityDetailsViewModelBase<PricingDetailsDto>(new LoadEntityDetailsCommand<PricingDto, PricingDetailsDto>(pricingStore)) {
    
    public static PricingDetailsViewModel LoadPricingDetailsViewModel(PricingStore pricingStore){
        var pricingDetailsViewModel = new PricingDetailsViewModel(pricingStore);
        
        pricingDetailsViewModel.LoadEntityCommand.Execute(pricingDetailsViewModel);
        
        return pricingDetailsViewModel;
    }
}