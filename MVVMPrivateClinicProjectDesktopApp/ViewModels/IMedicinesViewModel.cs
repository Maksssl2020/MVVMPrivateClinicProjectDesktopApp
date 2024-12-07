using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public interface IMedicinesViewModel {
    public void UpdateMedicines(IEnumerable<MedicineDto> medicinesDto);
}