using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadPrescriptionCommand(PrescriptionDetailsViewModel viewModel, PrescriptionStore prescriptionStore): AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await prescriptionStore.LoadPrescriptionById();
            viewModel.Prescription = prescriptionStore.SelectedPrescription;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}