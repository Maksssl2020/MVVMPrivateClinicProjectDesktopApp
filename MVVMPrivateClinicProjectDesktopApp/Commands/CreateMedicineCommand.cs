using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class CreateMedicineCommand(AddNewMedicineViewModel viewModel, MedicineStore medicineStore, Action resetForm) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        var saveMedicineRequest = new SaveMedicineRequest {
            Name = viewModel.MedicineName,
            Description = viewModel.MedicineDescription,
            Type = viewModel.MedicineType
        };
        
        await medicineStore.CreateMedicine(saveMedicineRequest);
        resetForm.Invoke();
    }
}