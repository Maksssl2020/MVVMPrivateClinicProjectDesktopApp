using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Base;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.ReferralRepository;

public interface IReferralRepository : IBaseRepository<ReferralDto> {
    Task<ReferralDto> SaveReferralAsync(SaveReferralRequest referralRequest);
    Task<ReferralDetailsDto?> GetReferralDetailsByIdAsync(int id);
    Task<IEnumerable<ReferralDto>> GetIssuedReferralsByPatientIdOrDoctorId(int personId, PersonType personType);
    Task<IEnumerable<ReferralDto>> GetAllReferralsDtoAsync();
    Task<int> CountIssuedReferralsAsync();
    Task<int> CountIssuedReferralsByDoctorIdAsync(int doctorId);
    Task<int> CountReferralTestUsesAsync(int referralTestId);
}