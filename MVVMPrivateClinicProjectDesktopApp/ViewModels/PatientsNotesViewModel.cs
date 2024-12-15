using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PatientsNotesViewModel : ViewModelBase {
    private readonly PatientNoteStore _patientNoteStore;
    private readonly AddSpecificDataToPatientStore _addSpecificDataToPatientStore;
    
    private readonly ObservableCollection<PatientNoteDto> _patientsNotesDto;
    public ICollectionView PatientsNotesView { get; set; }

    private ICommand LoadPatientsNotesCommand { get; set; }
    public ICommand ShowPatientNoteDetailsCommand { get; set; }
    public ICommand ShowSelectPatientToAddSpecificDataModal { get; set; }


    private PatientsNotesViewModel(PatientNoteStore patientNoteStore, AddSpecificDataToPatientStore addSpecificDataToPatientStore, ModalNavigationViewModel modalNavigationViewModel){
        _patientNoteStore = patientNoteStore;
        _addSpecificDataToPatientStore = addSpecificDataToPatientStore;
        
        _patientsNotesDto = [];
        PatientsNotesView  = CollectionViewSource.GetDefaultView(_patientsNotesDto);

        LoadPatientsNotesCommand = new LoadPatientsNotesDtoCommand(this, patientNoteStore);
        ShowPatientNoteDetailsCommand = modalNavigationViewModel.ShowPatientNoteDetailsModal;
        ShowSelectPatientToAddSpecificDataModal = modalNavigationViewModel.ShowSelectPatientToAddSpecificDataModal;
    }

    public static PatientsNotesViewModel LoadPatientNoteViewModel(PatientNoteStore patientNoteStore, AddSpecificDataToPatientStore addSpecificDataToPatientStore, ModalNavigationViewModel modalNavigationViewModel){
        var patientNoteViewModel = new PatientsNotesViewModel(patientNoteStore, addSpecificDataToPatientStore, modalNavigationViewModel);
        
        patientNoteViewModel.LoadPatientsNotesCommand.Execute(null);
        
        return patientNoteViewModel;
    }

    public void SetPatientNoteIdToShowDetails(int patientNoteId){
        _patientNoteStore.SelectedPatientNoteId = patientNoteId;
    }
    
    public void SetDataInAddSpecificDataToPatientStore(){
        _addSpecificDataToPatientStore.DataToAddName = "Patient Note";
        _addSpecificDataToPatientStore.DataColor =
            (SolidColorBrush) Application.Current.Resources["CustomBlueColor1"]!;
    }
    
    public void UpdatePatientsNotes(IEnumerable<PatientNoteDto> patientsNotesDto){
        _patientsNotesDto.Clear();

        foreach (var patientNoteDto in patientsNotesDto) {
            _patientsNotesDto.Add(patientNoteDto);
        }
    }
}