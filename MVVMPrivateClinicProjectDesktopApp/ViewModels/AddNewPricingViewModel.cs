using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using static MVVMPrivateClinicProjectDesktopApp.Helpers.RegexPatterns;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class AddNewPricingViewModel : AddNewEntityViewModelBase {
    private readonly ObservableCollection<ServiceTypeDto> _serviceTypes;
    public ICollectionView ServiceTypesView { get; set; }

    private string _serviceName = string.Empty;

    [Required(ErrorMessage = "Service Name is required!")]
    [RegularExpression(LettersOnlyRegexWithAdditionalCharacters, ErrorMessage = "Use letters only please!")]
    public string ServiceName {
        get => _serviceName;
        set {
            _serviceName = value;
            Validate(nameof(ServiceName), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }

    private ServiceTypeDto _serviceType = null!;

    [Required(ErrorMessage = "Service Type is required!")]
    public ServiceTypeDto ServiceType {
        get => _serviceType;
        set {
            _serviceType = value;
            Validate(nameof(ServiceType), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }
    
    private decimal _price;

    [Required(ErrorMessage = "Price is required!")]
    [Range(1.00, double.MaxValue, ErrorMessage = "Price must be greater than 0!")]
    public decimal Price {
        get => _price;
        set {
            _price = value;
            Validate(nameof(Price), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }
    
    private ICommand LoadServiceTypesCommand { get; set; }
    private ICommand CreatePricingCommand { get; set; }

    private AddNewPricingViewModel(PricingStore pricingStore){
        _serviceTypes = [];

        ServiceTypesView = CollectionViewSource.GetDefaultView(_serviceTypes);

        LoadServiceTypesCommand = new LoadServiceTypesDtoCommand(this, pricingStore);
        CreatePricingCommand = new CreatePricingCommand(this, pricingStore, ResetForm);
    }

    public static AddNewPricingViewModel LoadAddNewPricingViewModel(PricingStore pricingStore){
        var addNewPricingViewModel = new AddNewPricingViewModel(pricingStore);
        
        addNewPricingViewModel.LoadServiceTypesCommand.Execute(null);
        
        return addNewPricingViewModel;
    }
    
    protected override void Submit(){
        CreatePricingCommand.Execute(null);
    }

    protected override void ResetForm(){
        ServiceName = string.Empty;
        ServiceType = null!;
        Price = new decimal(0);
    }
    
    public void UpdateServiceTypes(IEnumerable<ServiceTypeDto> serviceTypes){
        _serviceTypes.Clear();

        foreach (var serviceType in serviceTypes) {
            _serviceTypes.Add(serviceType);
        }
    }
}