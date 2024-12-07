using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadMedicinesDtoCommand(IMedicinesViewModel viewModel, MedicineStore medicineStore) : AsyncRelayCommand{
    public override async Task ExecuteAsync(object? parameter){
        try {
            await medicineStore.LoadMedicinesDto();
            viewModel.UpdateMedicines(medicineStore.MedicinesDto);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}   