using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadDoctorIssuedPrescriptions(DoctorDetailsViewModel viewModel, PrescriptionStore prescriptionStore): AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            prescriptionStore.SelectedDoctorId = viewModel.SelectedDoctorId;
            await prescriptionStore.LoadDoctorIssuedPrescriptions();
            viewModel.UpdateIssuedPrescriptions(prescriptionStore.SelectedDoctorIssuedPrescriptions);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}