using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Interfaces;

public interface IReferralTestsViewModel {
    public void UpdateReferralTests(IEnumerable<ReferralTestDto> referralTests);
}