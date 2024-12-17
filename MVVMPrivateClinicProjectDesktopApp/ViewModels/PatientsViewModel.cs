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

public class PatientsViewModel : DisplayEntitiesViewModelBase<PatientDto> {
    private readonly PatientStore _patientStore;

    public ICommand ShowAddNewPatientModal {get; set;}
    public ICommand ShowDeletePatientModal { get; set; }
    public ICommand ShowPatientDataModal { get; set; }
    private ICommand LoadPatients { get; set; }
    
    private PatientsViewModel(PatientStore patientStore, ModalNavigationViewModel modalNavigationViewModel)
        :base([SortingOptions.AlphabeticalAscending, SortingOptions.AlphabeticalDescending, SortingOptions.IdAscending, SortingOptions.IdDescending]){
        _patientStore = patientStore;
        LoadPatients = new LoadPatientsCommand(UpdateEntities, patientStore);
        ShowAddNewPatientModal = modalNavigationViewModel.ShowAddNewPatientModal;
        ShowDeletePatientModal = modalNavigationViewModel.ShowDeletePatientModal;
        ShowPatientDataModal = modalNavigationViewModel.ShowPatientDataModal;
        
        _patientStore.PatientCreated += OnPatientCreated;
        _patientStore.PatientDeleted += OnPatientDeleted;
    }

    public static PatientsViewModel LoadPatientsViewModel(PatientStore patientStore,
        ModalNavigationViewModel modalNavigationViewModel){
        var patientsViewModel = new PatientsViewModel(patientStore, modalNavigationViewModel);
        
        patientsViewModel.LoadPatients.Execute(null);
        
        return patientsViewModel;
    }

    public override void UpdateEntities(IEnumerable<PatientDto> entities){
        Entities.Clear();

        foreach (var entity in entities) {
            Entities.Add(entity);
        }
        
        SelectedSortingOption = SortingOptions.AlphabeticalAscending;
    }

    protected override void SortEntities(){
        if (SelectedSortingOption is SortingOptions.AlphabeticalAscending or SortingOptions.AlphabeticalDescending) {
            ApplySortingOptions.ApplySortingWithTwoProperties(EntitiesView, SelectedSortingOption, nameof(PatientDto.FirstName), nameof(PatientDto.LastName));
        }
        else {
            ApplySortingOptions.ApplySortingWithOneProperty(EntitiesView, SelectedSortingOption, nameof(PatientDto.Id));
        }
    }

    protected override bool ApplyFilter(object obj){
        if (obj is not PatientDto patient) {
            return false;
        }

        if (string.IsNullOrWhiteSpace(Filter)) {
            return true;
        }
        
        var filter = Filter.Trim().ToLower();
        return patient.PatientCode.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
               patient.FirstName.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
               patient.LastName.Contains(filter, StringComparison.CurrentCultureIgnoreCase);
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
        Entities.Add(patient);
    }

    private void OnPatientDeleted(int patientId){
        var foundPatient = Entities.First(patient => patient.Id == patientId);
        Entities.Remove(foundPatient);
    }
}