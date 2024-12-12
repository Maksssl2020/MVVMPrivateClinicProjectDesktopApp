using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Referral;

public interface IReferralRepository {
    Task<ReferralDto> SaveReferralAsync(SaveReferralRequest referralRequest);
    Task<ReferralDetailsDto?> GetReferralDetailsByIdAsync(int id);
    Task<IEnumerable<ReferralDto>> GetPatientAllReferralsAsync(int patientId);
    Task<IEnumerable<ReferralDto>> GetAllReferralsDtoAsync();
}