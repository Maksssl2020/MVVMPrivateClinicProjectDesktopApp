using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PatientsNotesViewModel : DisplayEntitiesViewModelBase<PatientNoteDto, PatientNoteDetailsDto> {
    private readonly AddSpecificDataToPatientStore _addSpecificDataToPatientStore;
    
    public ICommand ShowPatientNoteDetailsCommand { get; set; }
    public ICommand ShowSelectPatientToAddSpecificDataModal { get; set; }

    private PatientsNotesViewModel(PatientNoteStore patientNoteStore, AddSpecificDataToPatientStore addSpecificDataToPatientStore, ModalNavigationViewModel modalNavigationViewModel)
        :base([SortingOptions.IdAscending, SortingOptions.IdDescending, SortingOptions.DateAscending, SortingOptions.DateDescending],
            patientNoteStore,
            modalNavigationViewModel) {
        _addSpecificDataToPatientStore = addSpecificDataToPatientStore;

        ShowPatientNoteDetailsCommand = modalNavigationViewModel.ShowPatientNoteDetailsModal;
        ShowSelectPatientToAddSpecificDataModal = modalNavigationViewModel.ShowSelectPatientToAddSpecificDataModal;
    }

    public static PatientsNotesViewModel LoadPatientNoteViewModel(PatientNoteStore patientNoteStore, AddSpecificDataToPatientStore addSpecificDataToPatientStore, ModalNavigationViewModel modalNavigationViewModel){
        var patientNoteViewModel = new PatientsNotesViewModel(patientNoteStore, addSpecificDataToPatientStore, modalNavigationViewModel);
        
        patientNoteViewModel.LoadEntitiesCommand.Execute(null);
        
        return patientNoteViewModel;
    }
    
    public void SetDataInAddSpecificDataToPatientStore(){
        _addSpecificDataToPatientStore.DataToAddName = "Patient Note";
        _addSpecificDataToPatientStore.DataColor =
            (SolidColorBrush) Application.Current.Resources["CustomBlueColor1"]!;
    }

    protected override void UpdateEntities(IEnumerable<PatientNoteDto> entities){
        Entities.Clear();

        foreach (var entity in entities) {
            Entities.Add(entity);
        }
        
        SelectedSortingOption = SortingOptions.IdAscending;
    }

    protected override void SortEntities(){
        ApplySortingOptions.ApplySortingWithOneProperty(EntitiesView, SelectedSortingOption,
            SelectedSortingOption is SortingOptions.IdAscending or SortingOptions.IdDescending
                ? nameof(PatientNoteDto.Id)
                : nameof(PatientNoteDto.DateIsuued));
    }

    protected override bool ApplyFilter(object obj){
        if (obj is not PatientNoteDto patientNoteDto) {
            return false;
        }
        
        if (string.IsNullOrWhiteSpace(Filter)) {
            return true;
        }
        
        var filter = Filter.Trim().ToLower();
        return patientNoteDto.PatientCode.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
               patientNoteDto.DoctorCode.Contains(filter, StringComparison.CurrentCultureIgnoreCase);
    }
}