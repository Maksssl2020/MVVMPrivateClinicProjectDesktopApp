using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PricingViewModel : DisplayEntitiesViewModelBase<PricingDto> {
    private ICommand LoadPricingCommand { get; set; }
    public ICommand ShowAddNewPricingViewCommand { get; set; }

    private PricingViewModel(PricingStore pricingStore, ModalNavigationViewModel modalNavigationViewModel) 
        :base([SortingOptions.IdAscending, SortingOptions.IdDescending, SortingOptions.PriceAscending, SortingOptions.PriceDescending]){
        LoadPricingCommand = new LoadPricingCommand(UpdateEntities, pricingStore);
        ShowAddNewPricingViewCommand = modalNavigationViewModel.ShowAddNewPricingModal;
        pricingStore.PricingCreated += OnPricingCreated;
    }

    public static PricingViewModel LoadPricingViewModel(PricingStore pricingStore, ModalNavigationViewModel modalNavigationViewModel){
        var pricingViewModel = new PricingViewModel(pricingStore, modalNavigationViewModel);
        
        pricingViewModel.LoadPricingCommand.Execute(null);
        
        return pricingViewModel;
    }

    public override void UpdateEntities(IEnumerable<PricingDto> entities){
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
    
    private void OnPricingCreated(PricingDto pricingDto){
        Entities.Add(pricingDto);
    }
}