using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Base;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.MedicineRepository;

public interface IMedicineRepository : IBaseRepository<MedicineDto> {
    Task<MedicineDto> SaveMedicineAsync(SaveMedicineRequest medicineRequest);
    Task<MedicineDetailsDto> GetMedicineDetailsByIdAsync(int medicineId);
    Task<IEnumerable<MedicineDto>> GetAllMedicinesDtoAsync();
    Task<IEnumerable<MedicineTypeDto>> GetAllExistingMedicineTypesAsync();
}