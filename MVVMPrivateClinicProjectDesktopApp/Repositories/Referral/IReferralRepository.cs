using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Referral;

public interface IReferralRepository {
    Task<ReferralDto> SaveReferralAsync(SaveReferralRequest referralRequest);
    Task<ReferralDetailsDto?> GetReferralDetailsByIdAsync(int id);
    Task<IEnumerable<ReferralDto>> GetIssuedReferralsByPatientIdOrDoctorId(int personId, PersonType personType);
    Task<IEnumerable<ReferralDto>> GetAllReferralsDtoAsync();
    Task<int> CountIssuedReferralsAsync();
    Task<int> CountIssuedReferralsByDoctorIdAsync(int doctorId);
    Task<int> CountReferralTestUsesAsync(int referralTestId);
}