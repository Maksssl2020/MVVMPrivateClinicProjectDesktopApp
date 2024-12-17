using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadPrescriptionsDtoCommand(PrescriptionsViewModel viewModel, PrescriptionStore prescriptionStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {   
            await prescriptionStore.LoadPrescriptions();
            viewModel.UpdateEntities(prescriptionStore.PrescriptionsDto);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}