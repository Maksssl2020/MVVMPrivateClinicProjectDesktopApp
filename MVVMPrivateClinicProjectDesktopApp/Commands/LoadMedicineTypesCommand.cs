using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadMedicineTypesCommand(AddNewMedicineViewModel viewModel, MedicineStore medicineStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await medicineStore.LoadMedicineTypes();
            viewModel.UpdateMedicineTypes(medicineStore.MedicineTypes);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}