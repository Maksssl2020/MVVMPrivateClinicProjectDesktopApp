using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Medicine;

public interface IMedicineRepository {
    Task<MedicineDto> SaveMedicineAsync(SaveMedicineRequest medicineRequest);
    Task<MedicineDto> GetMedicineByIdAsync(int medicineId);
    Task<IEnumerable<MedicineDto>> GetAllMedicinesDtoAsync();
    Task<IEnumerable<MedicineTypeDto>> GetAllExistingMedicineTypesAsync();
}