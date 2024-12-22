using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadDoctorIssuedPatientNotes(DoctorDetailsViewModel viewModel, PatientNoteStore patientNoteStore): AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            patientNoteStore.SelectedDoctorId = viewModel.SelectedDoctorId;
            await patientNoteStore.LoadSelectedDoctorIssuedPatientNotes();
            viewModel.UpdateIssuedPatientNotes(patientNoteStore.SelectedDoctorIssuedPatientNotes);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}