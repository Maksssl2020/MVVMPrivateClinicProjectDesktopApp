using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PatientsNotesViewModel : ViewModelBase {
    private readonly ObservableCollection<PatientNoteDto> _patientsNotesDto;
    public ICollectionView PatientsNotesView { get; set; }

    private ICommand LoadPatientsNotesCommand { get; set; }
    private ICommand ShowAddNewPatientNoteCommand { get; set; }

    private PatientsNotesViewModel(PatientNoteStore patientNoteStore, PatientDataModalNavigationViewModel patientDataModalNavigationViewModel){
        _patientsNotesDto = [];
        PatientsNotesView  = CollectionViewSource.GetDefaultView(_patientsNotesDto);

        LoadPatientsNotesCommand = new LoadPatientsNotesDtoCommand(this, patientNoteStore);
        ShowAddNewPatientNoteCommand = patientDataModalNavigationViewModel.ShowAddNewPatientNoteViewCommand;
    }

    public static PatientsNotesViewModel LoadPatientNoteViewModel(PatientNoteStore patientNoteStore, PatientDataModalNavigationViewModel patientDataModalNavigationViewModel){
        var patientNoteViewModel = new PatientsNotesViewModel(patientNoteStore, patientDataModalNavigationViewModel);
        
        patientNoteViewModel.LoadPatientsNotesCommand.Execute(null);
        
        return patientNoteViewModel;
    }
    
    public void UpdatePatientsNotes(IEnumerable<PatientNoteDto> patientsNotesDto){
        _patientsNotesDto.Clear();

        foreach (var patientNoteDto in patientsNotesDto) {
            _patientsNotesDto.Add(patientNoteDto);
        }
    }
}