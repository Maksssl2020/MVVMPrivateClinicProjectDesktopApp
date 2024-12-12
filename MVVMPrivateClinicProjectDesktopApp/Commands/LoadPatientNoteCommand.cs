using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadPatientNoteCommand(PatientNoteDetailsViewModel viewModel, PatientNoteStore patientNoteStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await patientNoteStore.LoadSelectedPatientNote();
            viewModel.UpdatePatientNoteDetails(patientNoteStore.SelectedPatientNote);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}