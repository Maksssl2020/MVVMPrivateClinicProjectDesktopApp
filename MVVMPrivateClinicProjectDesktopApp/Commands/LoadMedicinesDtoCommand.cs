using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadMedicinesDtoCommand(Action<IEnumerable<MedicineDto>> updateMedicines, MedicineStore medicineStore) : AsyncRelayCommand{
    public override async Task ExecuteAsync(object? parameter){
        try {
            await medicineStore.LoadMedicinesDto();
            updateMedicines.Invoke(medicineStore.MedicinesDto);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}   