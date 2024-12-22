using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.ReferralTest;

public interface IReferralTestRepository {
    Task<ReferralTestDto?> GetReferralTestByIdAsync(int referralTestId);
    Task<ReferralTestDetailsDto?> GetReferralTestDetailsByIdAsync(int referralTestId);
    Task<IEnumerable<ReferralTestDto>> GetAllReferralTestsDtoAsync();
}