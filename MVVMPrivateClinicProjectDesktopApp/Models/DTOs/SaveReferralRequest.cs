namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class SaveReferralRequest {
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int? DiseaseId { get; set; }
    public int ReferralTestId { get; set; }
}