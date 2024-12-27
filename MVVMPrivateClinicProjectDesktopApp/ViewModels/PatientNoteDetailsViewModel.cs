using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PatientNoteDetailsViewModel : EntityDetailsViewModelBase<PatientNoteDetailsDto> {
    private PatientNoteDetailsViewModel(PatientNoteStore patientNoteStore)
        :base(new LoadEntityDetailsCommand<PatientNoteDto, PatientNoteDetailsDto>(patientNoteStore)){
    }

    public static PatientNoteDetailsViewModel LoadPatientNoteDetailsViewModel(PatientNoteStore patientNoteStore){
        var patientNoteDetailsViewModel = new PatientNoteDetailsViewModel(patientNoteStore);
        
        patientNoteDetailsViewModel.LoadEntityCommand.Execute(patientNoteDetailsViewModel);
        
        return patientNoteDetailsViewModel;
    }
}