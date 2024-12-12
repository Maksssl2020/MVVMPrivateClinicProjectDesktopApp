using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PatientNoteDetailsViewModel : ViewModelBase {
    
    private PatientNoteDetailsDto _patientNoteDetailsDto = null!;
    public PatientNoteDetailsDto PatientNote {
        get => _patientNoteDetailsDto;
        set {
            _patientNoteDetailsDto = value;
            OnPropertyChanged();
        }
    }

    private ICommand LoadPatientNoteCommand { get; }

    private PatientNoteDetailsViewModel(PatientNoteStore patientNoteStore){
        LoadPatientNoteCommand = new LoadPatientNoteCommand(this, patientNoteStore);
    }

    public static PatientNoteDetailsViewModel LoadPatientNoteDetailsViewModel(PatientNoteStore patientNoteStore){
        var patientNoteDetailsViewModel = new PatientNoteDetailsViewModel(patientNoteStore);
        
        patientNoteDetailsViewModel.LoadPatientNoteCommand.Execute(null);
        
        return patientNoteDetailsViewModel;
    }
    
    public void UpdatePatientNoteDetails(PatientNoteDetailsDto selectedPatientNote){
        PatientNote = selectedPatientNote;
    }
}