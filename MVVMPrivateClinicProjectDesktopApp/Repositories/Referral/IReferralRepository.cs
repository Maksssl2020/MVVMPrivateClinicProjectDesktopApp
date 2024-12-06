using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Referral;

public interface IReferralRepository {
    Task<IEnumerable<ReferralDto>> GetAllReferralsDtoAsync();
}