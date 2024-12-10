namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class SaveDiagnosisRequest {
    public required string Description { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int DiseaseId { get; set; }
}