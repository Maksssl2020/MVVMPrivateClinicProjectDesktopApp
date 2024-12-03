using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadMedicinesDtoCommand(IssuePrescriptionViewModel viewModel, MedicineStore medicineStore) : AsyncRelayCommand{
    public override async Task ExecuteAsync(object? parameter){
        try {
            await medicineStore.LoadMedicinesDto();
            viewModel.UpdateMedicinesDto(medicineStore.MedicinesDto);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}