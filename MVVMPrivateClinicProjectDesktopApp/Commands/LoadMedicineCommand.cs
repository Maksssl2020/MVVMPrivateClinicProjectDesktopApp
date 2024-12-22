using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadMedicineCommand(MedicineDetailsViewModel viewModel, MedicineStore medicineStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await medicineStore.LoadMedicine();
            viewModel.MedicineDetails = medicineStore.MedicineDetails;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}