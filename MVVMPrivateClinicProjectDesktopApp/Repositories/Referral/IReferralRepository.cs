using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Referral;

public interface IReferralRepository {
    Task<ReferralDto> SaveReferral(SaveReferralRequest referralRequest);
    Task<IEnumerable<ReferralDto>> GetPatientAllReferrals(int patientId);
    Task<IEnumerable<ReferralDto>> GetAllReferralsDtoAsync();
}