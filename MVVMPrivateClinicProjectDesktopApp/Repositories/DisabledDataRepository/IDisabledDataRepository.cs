using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.DisabledDataRepository;

public interface IDisabledDataRepository {
    Task<IEnumerable<DisabledDataDto>> GetDisabledDataAsync();
    Task RestoreDataAsync(int dataId);
}