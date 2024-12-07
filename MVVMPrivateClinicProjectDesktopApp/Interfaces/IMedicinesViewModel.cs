using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Interfaces;

public interface IMedicinesViewModel {
    public void UpdateMedicines(IEnumerable<MedicineDto> medicinesDto);
}