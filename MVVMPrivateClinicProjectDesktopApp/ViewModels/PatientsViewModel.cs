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

public class PatientsViewModel : ViewModelBase, IPatientViewModel {
    private readonly PatientStore _patientStore;

    public ICommand ShowAddNewPatientModal {get; set;}
    public ICommand ShowDeletePatientModal { get; set; }
    public ICommand ShowPatientDataModal { get; set; }
    private ICommand LoadPatients { get; set; }
    
    private readonly ObservableCollection<PatientDto> _patients = [];
    public ICollectionView PatientsView { get; set; }

    public ObservableCollection<SortingOptions> SortingOptionsList { get; } = [SortingOptions.AlphabeticalAscending, SortingOptions.AlphabeticalDescending, SortingOptions.IdAscending, SortingOptions.IdDescending];
    
    private SortingOptions _selectedSortingOption;
    public SortingOptions SelectedSortingOption {
        get => _selectedSortingOption;
        set {
            _selectedSortingOption = value;
            OnPropertyChanged();
            SortPatients();
            PatientsView.Refresh();
        }
    }
    
    private string _patientsFilter = string.Empty;
    public string PatientsFilter {
        get => _patientsFilter;
        set {
            _patientsFilter = value;
            OnPropertyChanged();
            PatientsView.Refresh();
        }
    }

    private PatientsViewModel(PatientStore patientStore, ModalNavigationViewModel modalNavigationViewModel){
        _patientStore = patientStore;
        LoadPatients = new LoadPatientsCommand(this, patientStore);
        ShowAddNewPatientModal = modalNavigationViewModel.ShowAddNewPatientModal;
        ShowDeletePatientModal = modalNavigationViewModel.ShowDeletePatientModal;
        ShowPatientDataModal = modalNavigationViewModel.ShowPatientDataModal;
        
        PatientsView = CollectionViewSource.GetDefaultView(_patients);
        PatientsView.Filter = FilterPatients;
        
        _patientStore.PatientCreated += OnPatientCreated;
        _patientStore.PatientDeleted += OnPatientDeleted;
    }

    public static PatientsViewModel LoadPatientsViewModel(PatientStore patientStore,
        ModalNavigationViewModel modalNavigationViewModel){
        var patientsViewModel = new PatientsViewModel(patientStore, modalNavigationViewModel);
        
        patientsViewModel.LoadPatients.Execute(null);
        
        return patientsViewModel;
    }

    public void SetPatientIdToDelete(int patientId) {
        _patientStore.PatientIdToDelete = patientId;
    }

    public void SetPatientIdToShowDetails(int patientId){
        _patientStore.PatientIdToShowDetails = patientId;
    }
    
    public override void Dispose(){
        _patientStore.PatientCreated -= OnPatientCreated;
        _patientStore.PatientDeleted -= OnPatientDeleted;
        base.Dispose();
    }

    private void OnPatientCreated(PatientDto patient){
        _patients.Add(patient);
    }

    private void OnPatientDeleted(int patientId){
        var foundPatient = _patients.First(patient => patient.Id == patientId);
        _patients.Remove(foundPatient);
    }

    public void UpdatePatients(IEnumerable<PatientDto> patients){
        _patients.Clear();

        foreach (var patient in patients) {
            _patients.Add(patient);
        }
        
        SelectedSortingOption = SortingOptions.AlphabeticalAscending;
    }

    private void SortPatients(){
        PatientsView.SortDescriptions.Clear();
        
        switch (SelectedSortingOption) {
            case SortingOptions.AlphabeticalAscending: {
                PatientsView.SortDescriptions.Add(
                    new SortDescription(nameof(PatientDto.FirstName), ListSortDirection.Ascending)
                ); 
                PatientsView.SortDescriptions.Add(
                    new SortDescription(nameof(PatientDto.LastName), ListSortDirection.Ascending)
                ); 
                break;
            }
            case SortingOptions.AlphabeticalDescending: {
                PatientsView.SortDescriptions.Add(
                    new SortDescription(nameof(PatientDto.FirstName), ListSortDirection.Descending)
                ); 
                PatientsView.SortDescriptions.Add(
                    new SortDescription(nameof(PatientDto.LastName), ListSortDirection.Descending)
                ); 
                break;
            }
            case SortingOptions.IdAscending: {
                PatientsView.SortDescriptions.Add(
                    new SortDescription(nameof(PatientDto.Id), ListSortDirection.Ascending)
                ); 
                break;
            }
            case SortingOptions.IdDescending: {
                PatientsView.SortDescriptions.Add(
                    new SortDescription(nameof(PatientDto.Id), ListSortDirection.Descending)
                ); 
                break;
            }
            default: {
                PatientsView.SortDescriptions.Add(
                    new SortDescription(nameof(PatientDto.FirstName), ListSortDirection.Ascending)
                ); 
                PatientsView.SortDescriptions.Add(
                    new SortDescription(nameof(PatientDto.LastName), ListSortDirection.Ascending)
                ); 
                break;
            }
        }
    }
    
    private bool FilterPatients(object obj){
        if (obj is not PatientDto patient) {
            return false;
        }

        if (string.IsNullOrWhiteSpace(PatientsFilter)) {
            return true;
        }
        
        var filter = PatientsFilter.Trim().ToLower();
        return patient.PatientCode.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
               patient.FirstName.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
               patient.LastName.Contains(filter, StringComparison.CurrentCultureIgnoreCase);
    }
}