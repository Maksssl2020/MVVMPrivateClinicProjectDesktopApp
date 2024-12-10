namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class DiagnosisDto {
    public int Id { get; set; }
    public DateOnly DiagnosisDate { get; set; }
    public required string Description { get; set; }
    public required string PatientCode { get; set; }
    public required string DoctorCode { get; set; }
    public required string DiseaseCode { get; set; }
}