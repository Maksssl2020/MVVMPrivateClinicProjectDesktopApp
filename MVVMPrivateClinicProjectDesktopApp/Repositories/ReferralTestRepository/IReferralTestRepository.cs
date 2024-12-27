using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Base;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.ReferralTestRepository;

public interface IReferralTestRepository : IBaseRepository<ReferralTestDto> {
    Task<ReferralTestDto> SaveReferralTestAsync(SaveReferralTestRequest referralTestRequest);
    Task<ReferralTestDto?> GetReferralTestByIdAsync(int referralTestId);
    Task<ReferralTestDetailsDto?> GetReferralTestDetailsByIdAsync(int referralTestId);
    Task<IEnumerable<ReferralTestDto>> GetAllReferralTestsDtoAsync();
}