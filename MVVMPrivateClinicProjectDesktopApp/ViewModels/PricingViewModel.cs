using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PricingViewModel : DisplayEntitiesViewModelBase<PricingDto, PricingDetailsDto> {
    public ICommand ShowAddNewPricingViewCommand { get; set; }
    public ICommand ShowPricingDetailsModalCommand { get; set; }

    private PricingViewModel(PricingStore pricingStore, ModalNavigationViewModel modalNavigationViewModel) 
        :base([SortingOptions.IdAscending, SortingOptions.IdDescending, SortingOptions.PriceAscending, SortingOptions.PriceDescending],
            pricingStore,
            modalNavigationViewModel) {
        
        ShowAddNewPricingViewCommand = modalNavigationViewModel.ShowAddNewPricingModal;
        ShowPricingDetailsModalCommand = modalNavigationViewModel.ShowPricingDetailsModal;
    }

    public static PricingViewModel LoadPricingViewModel(PricingStore pricingStore, ModalNavigationViewModel modalNavigationViewModel){
        var pricingViewModel = new PricingViewModel(pricingStore, modalNavigationViewModel);
        
        pricingViewModel.LoadEntitiesCommand.Execute(null);
        
        return pricingViewModel;
    }

    protected override void UpdateEntities(IEnumerable<PricingDto> entities){
        Entities.Clear();

        foreach (var entity in entities) {
            Entities.Add(entity);
        }

        SelectedSortingOption = SortingOptions.IdAscending;
    }

    protected override void SortEntities(){
        ApplySortingOptions.ApplySortingWithOneProperty(EntitiesView, SelectedSortingOption,
            SelectedSortingOption is SortingOptions.IdAscending or SortingOptions.IdDescending
                ? nameof(PricingDto.Id)
                : nameof(PricingDto.Price));
    }

    protected override bool ApplyFilter(object obj){
        if (obj is not PricingDto pricingDto) {
            return false;
        }

        if (string.IsNullOrWhiteSpace(Filter)) {
            return true;
        }
        
        var filter = Filter.Trim().ToLower();
        return pricingDto.ServiceName.Contains(filter, StringComparison.CurrentCultureIgnoreCase);
    }

}