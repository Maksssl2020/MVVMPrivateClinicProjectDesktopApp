using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadSelectedPatientNotesDtoCommand(AddNewPatientNoteViewModel viewModel, PatientNoteStore patientNoteStore, PatientStore patientStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            patientNoteStore.SelectedPatientId = patientStore.EntityIdToShowDetails;
            await patientNoteStore.LoadSelectedPatientNotes();
            viewModel.UpdateSelectedPatientNotesDto(patientNoteStore.SelectedPatientNotes);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}