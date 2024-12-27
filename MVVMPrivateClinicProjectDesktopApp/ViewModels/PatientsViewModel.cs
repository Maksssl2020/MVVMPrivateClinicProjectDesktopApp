using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PatientsViewModel : DisplayEntitiesViewModelBase<PatientDto, PatientDto> {
    public ICommand ShowAddNewPatientModal {get; set;}
    public ICommand ShowPatientDataModal { get; set; }
    
    private PatientsViewModel(PatientStore patientStore, ModalNavigationViewModel modalNavigationViewModel)
        :base([SortingOptions.AlphabeticalAscending, SortingOptions.AlphabeticalDescending, SortingOptions.IdAscending, SortingOptions.IdDescending],
            patientStore,
            modalNavigationViewModel){
        ShowAddNewPatientModal = modalNavigationViewModel.ShowAddNewPatientModal;
        ShowPatientDataModal = modalNavigationViewModel.ShowPatientDataModal;
    }

    public static PatientsViewModel LoadPatientsViewModel(PatientStore patientStore,
        ModalNavigationViewModel modalNavigationViewModel){
        var patientsViewModel = new PatientsViewModel(patientStore, modalNavigationViewModel);
        
        patientsViewModel.LoadEntitiesCommand.Execute(null);
        
        return patientsViewModel;
    }

    protected override void UpdateEntities(IEnumerable<PatientDto> entities){
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
}