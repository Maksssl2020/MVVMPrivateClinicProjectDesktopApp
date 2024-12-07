using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PricingViewModel : ViewModelBase {
    private readonly ObservableCollection<PricingDto> _pricingDto;
    public ICollectionView PricingView { get; set; }

    private ICommand LoadPricingCommand { get; set; }
    public ICommand ShowAddNewPricingViewCommand { get; set; }

    private PricingViewModel(PricingStore pricingStore, ModalNavigationViewModel modalNavigationViewModel){
        _pricingDto = [];
        PricingView = CollectionViewSource.GetDefaultView(_pricingDto);

        LoadPricingCommand = new LoadPricingDtoCommand(this, pricingStore);
        ShowAddNewPricingViewCommand = modalNavigationViewModel.ShowAddNewPricingModal;
        pricingStore.PricingCreated += OnPricingCreated;
    }

    public static PricingViewModel LoadPricingViewModel(PricingStore pricingStore, ModalNavigationViewModel modalNavigationViewModel){
        var pricingViewModel = new PricingViewModel(pricingStore, modalNavigationViewModel);
        
        pricingViewModel.LoadPricingCommand.Execute(null);
        
        return pricingViewModel;
    }

    private void OnPricingCreated(PricingDto pricingDto){
        _pricingDto.Add(pricingDto);
    }
    
    public void UpdatePricing(IEnumerable<PricingDto> pricingDto){
        _pricingDto.Clear();

        foreach (var pricing in pricingDto) {
            _pricingDto.Add(pricing);
        }
    }
}