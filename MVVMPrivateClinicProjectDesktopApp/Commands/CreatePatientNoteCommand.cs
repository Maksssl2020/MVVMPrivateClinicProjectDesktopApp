using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class CreatePatientNoteCommand(AddNewPatientNoteViewModel viewModel, PatientNoteStore patientNoteStore, Action resetForm) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        var savePatientNoteRequest = new SavePatientNoteRequest {
            Description = viewModel.PatientNoteDescription,
            IdDoctor = viewModel.SelectedDoctor.Id,
            IdPatient = viewModel.SelectedPatientId
        };

        await patientNoteStore.CreatePatientNote(savePatientNoteRequest);
        resetForm.Invoke();
    }
}