using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.ReferralTest;

public interface IReferralTestRepository {
    Task<ReferralTestDto?> GetReferralTestByIdAsync(int referralTestId);
    Task<IEnumerable<ReferralTestDto>> GetAllReferralTestsDtoAsync();
}