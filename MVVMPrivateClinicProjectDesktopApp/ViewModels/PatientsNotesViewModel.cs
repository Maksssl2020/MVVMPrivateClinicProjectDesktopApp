using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PatientsNotesViewModel : ViewModelBase {
    private readonly PatientNoteStore _patientNoteStore;
    
    private readonly ObservableCollection<PatientNoteDto> _patientsNotesDto;
    public ICollectionView PatientsNotesView { get; set; }

    private ICommand LoadPatientsNotesCommand { get; set; }
    public ICommand ShowPatientNoteDetailsCommand { get; set; }

    private PatientsNotesViewModel(PatientNoteStore patientNoteStore, ModalNavigationViewModel modalNavigationViewModel){
        _patientNoteStore = patientNoteStore;
        
        _patientsNotesDto = [];
        PatientsNotesView  = CollectionViewSource.GetDefaultView(_patientsNotesDto);

        LoadPatientsNotesCommand = new LoadPatientsNotesDtoCommand(this, patientNoteStore);
        ShowPatientNoteDetailsCommand = modalNavigationViewModel.ShowPatientNoteDetailsModal;
    }

    public static PatientsNotesViewModel LoadPatientNoteViewModel(PatientNoteStore patientNoteStore, ModalNavigationViewModel modalNavigationViewModel){
        var patientNoteViewModel = new PatientsNotesViewModel(patientNoteStore, modalNavigationViewModel);
        
        patientNoteViewModel.LoadPatientsNotesCommand.Execute(null);
        
        return patientNoteViewModel;
    }

    public void SetPatientNoteIdToShowDetails(int patientNoteId){
        _patientNoteStore.SelectedPatientNoteId = patientNoteId;
    }
    
    public void UpdatePatientsNotes(IEnumerable<PatientNoteDto> patientsNotesDto){
        _patientsNotesDto.Clear();

        foreach (var patientNoteDto in patientsNotesDto) {
            _patientsNotesDto.Add(patientNoteDto);
        }
    }
}