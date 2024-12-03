using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Medicine;

public interface IMedicineRepository {
    Task<IEnumerable<Models.Entities.Medicine>> GetAllMedicinesAsync();
    Task<IEnumerable<MedicineDto>> GetAllMedicinesDtoAsync();
}