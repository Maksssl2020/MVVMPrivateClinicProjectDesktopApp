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

    private PatientsNotesViewModel(PatientNoteStore patientNoteStore){
        _patientNoteStore = patientNoteStore;

        _patientsNotesDto = [];
        PatientsNotesView  = CollectionViewSource.GetDefaultView(_patientsNotesDto);

        LoadPatientsNotesCommand = new LoadPatientsNotesDtoCommand(this, _patientNoteStore);
    }

    public static PatientsNotesViewModel LoadPatientNoteViewModel(PatientNoteStore patientNoteStore){
        var patientNoteViewModel = new PatientsNotesViewModel(patientNoteStore);
        
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