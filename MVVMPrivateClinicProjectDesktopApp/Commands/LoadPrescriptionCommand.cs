using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadPrescriptionCommand(PrescriptionDetailsViewModel viewModel, PrescriptionStore prescriptionStore): AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await prescriptionStore.LoadPrescriptionById();
            Console.WriteLine("Prescription Loaded");
            viewModel.UpdatePrescription(prescriptionStore.SelectedPrescription);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}