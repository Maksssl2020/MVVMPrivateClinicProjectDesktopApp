using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.ReferralTest;

public interface IReferralTestRepository {
    Task<IEnumerable<ReferralTestDto>> GetAllReferralTestsDtoAsync();
}