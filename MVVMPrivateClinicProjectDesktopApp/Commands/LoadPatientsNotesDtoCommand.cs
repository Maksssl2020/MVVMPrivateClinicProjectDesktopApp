using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadPatientsNotesDtoCommand(PatientsNotesViewModel viewModel, PatientNoteStore patientNoteStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await patientNoteStore.LoadPatientsNotes();
            viewModel.UpdatePatientsNotes(patientNoteStore.PatientsNotesDto);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}