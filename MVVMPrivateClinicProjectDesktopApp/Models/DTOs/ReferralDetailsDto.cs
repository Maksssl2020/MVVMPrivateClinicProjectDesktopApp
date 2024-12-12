namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class ReferralDetailsDto {
    public int Id { get; set; }
    public DateTime DateIssued { get; set; }
    public required string Name { get; set; }
    public PatientDetailsDto PatientDetailsDto { get; set; } = null!;
    public DoctorDetailsDto DoctorDetailsDto { get; set; } = null!;
    public DiseaseDto DiseaseDetailsDto { get; set; } = null!;
    public ReferralTestDto ReferralTestDetailsDto { get; set; } = null!;
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
    public int IdDisease { get; set; }
    public int IdReferralTest { get; set; }
}