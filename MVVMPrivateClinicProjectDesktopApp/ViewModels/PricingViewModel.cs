using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PricingViewModel : ViewModelBase {
    private readonly PricingStore _pricingStore;
    
    private readonly ObservableCollection<PricingDto> _pricingDto;
    public ICollectionView PricingView { get; set; }

    private ICommand LoadPricingCommand { get; set; }

    private PricingViewModel(PricingStore pricingStore){
        _pricingStore = pricingStore;
        
        _pricingDto = [];
        PricingView = CollectionViewSource.GetDefaultView(_pricingDto);

        LoadPricingCommand = new LoadPricingDtoCommand(this, _pricingStore);
    }

    public static PricingViewModel LoadPricingViewModel(PricingStore pricingStore){
        var pricingViewModel = new PricingViewModel(pricingStore);
        
        pricingViewModel.LoadPricingCommand.Execute(null);
        
        return pricingViewModel;
    }

    public void UpdatePricing(IEnumerable<PricingDto> pricingDto){
        _pricingDto.Clear();

        foreach (var pricing in pricingDto) {
            _pricingDto.Add(pricing);
        }
    }
}