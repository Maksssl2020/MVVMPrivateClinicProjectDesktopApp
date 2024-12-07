using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class AddNewPricingViewModel : ViewModelBase {
    private readonly ObservableCollection<ServiceTypeDto> _serviceTypes;
    public ICollectionView ServiceTypesView { get; set; }

    private string _serviceName = string.Empty;

    [Required(ErrorMessage = "Service Name is required!")]
    [RegularExpression(@"([\p{L}]+[\s]?)+", ErrorMessage = "Use letters only please!")]
    public string ServiceName {
        get => _serviceName;
        set {
            _serviceName = value;
            Validate(nameof(ServiceName), value);
            SubmitCommand.OnCanExecuteChanged();
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
        }
    }
    
    private ICommand LoadServiceTypesCommand { get; set; }
    public SubmitCommand SubmitCommand { get; set; }
    private ICommand CreatePricingCommand { get; set; }

    private AddNewPricingViewModel(PricingStore pricingStore){
        _serviceTypes = [];

        ServiceTypesView = CollectionViewSource.GetDefaultView(_serviceTypes);

        LoadServiceTypesCommand = new LoadServiceTypesDtoCommand(this, pricingStore);
        SubmitCommand = new SubmitCommand(Submit, CanSubmit);
        CreatePricingCommand = new CreatePricingCommand(this, pricingStore, ResetForm);
    }

    public static AddNewPricingViewModel LoadAddNewPricingViewModel(PricingStore pricingStore){
        var addNewPricingViewModel = new AddNewPricingViewModel(pricingStore);
        
        addNewPricingViewModel.LoadServiceTypesCommand.Execute(null);
        
        return addNewPricingViewModel;
    }
    
    private bool CanSubmit(){
        var context = new ValidationContext(this);
        var results = new List<ValidationResult>();
        return Validator.TryValidateObject(this, context, results, true);
    }

    private void Submit(){
        CreatePricingCommand.Execute(null);
    }

    private void ResetForm(){
        ServiceName = string.Empty;
        ServiceType = null!;
        _price = new decimal(0);
    }
    
    public void UpdateServiceTypes(IEnumerable<ServiceTypeDto> serviceTypes){
        _serviceTypes.Clear();

        foreach (var serviceType in serviceTypes) {
            _serviceTypes.Add(serviceType);
        }
    }
}