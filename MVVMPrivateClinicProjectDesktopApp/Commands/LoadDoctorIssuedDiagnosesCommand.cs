using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadDoctorIssuedDiagnosesCommand(DoctorDetailsViewModel viewModel, DiagnosisStore diagnosisStore): AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            diagnosisStore.SelectedDoctorId = viewModel.SelectedDoctorId;
            await diagnosisStore.LoadDoctorIssuedDiagnoses();
            viewModel.UpdateIssuedDiagnoses(diagnosisStore.DoctorIssuedDiagnoses);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}