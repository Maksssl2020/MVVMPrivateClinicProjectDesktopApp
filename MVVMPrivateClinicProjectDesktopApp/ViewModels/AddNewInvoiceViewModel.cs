using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class AddNewInvoiceViewModel : AddNewEntityViewModelBase {
    
    private readonly ObservableCollection<PatientDto> _patients;
    private readonly ObservableCollection<PricingDto> _pricing;
    public ICollectionView PatientsView { get; set; }
    public ICollectionView PricingView { get; set; }
    
    private PatientDto _selectedPatient = null!;

    [Required(ErrorMessage = "Patient is required!")]
    public PatientDto SelectedPatient {
        get => _selectedPatient;
        set  {
            _selectedPatient = value;
            Validate(nameof(SelectedPatient), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }
    
    private PricingDto _selectedPricing = null!;
    
    [Required(ErrorMessage = "Pricing is required!")]
    public PricingDto SelectedPricing {
        get => _selectedPricing;
        set {
            _selectedPricing = value;
            Validate(nameof(SelectedPricing), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }

    private ICommand LoadPatientsCommand { get; set; }
    private ICommand LoadPricingCommand { get; set; }
    private ICommand CreateInvoiceCommand { get; set; }

    private AddNewInvoiceViewModel(InvoiceStore invoiceStore, PatientStore patientStore, PricingStore pricingStore){
        _patients = [];
        _pricing = [];
        
        PatientsView = CollectionViewSource.GetDefaultView(_patients);
        PricingView = CollectionViewSource.GetDefaultView(_pricing);

        LoadPatientsCommand = new LoadEntitiesCommand<PatientDto, PatientDto>(UpdatePatients, patientStore);
        LoadPricingCommand = new LoadEntitiesCommand<PricingDto, PricingDetailsDto>(UpdatePricing, pricingStore);
        CreateInvoiceCommand = new CreateInvoiceCommand(this, invoiceStore, ResetForm);
    }

    public static AddNewInvoiceViewModel LoadAddNewInvoiceViewModel(InvoiceStore invoiceStore, PatientStore patientStore, PricingStore pricingStore){
        var addNewInvoiceViewModel = new AddNewInvoiceViewModel(invoiceStore, patientStore, pricingStore);
        
        addNewInvoiceViewModel.LoadPatientsCommand.Execute(null);
        addNewInvoiceViewModel.LoadPricingCommand.Execute(null);
        
        return addNewInvoiceViewModel;
    }

    private void UpdatePatients(IEnumerable<PatientDto> patients){
        _patients.Clear();

        foreach (var patient in patients) {
            _patients.Add(patient);
        }
    }
    
    private void UpdatePricing(IEnumerable<PricingDto> pricing){
        _pricing.Clear();

        foreach (var pricingDto in pricing) {
            _pricing.Add(pricingDto);
        }
    }

    protected override void Submit(){
        CreateInvoiceCommand.Execute(null);
    }

    protected override void ResetForm(){
        SelectedPatient = null!;
        SelectedPricing = null!;
    }
}