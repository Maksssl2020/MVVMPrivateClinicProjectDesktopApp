namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class SavePatientNoteRequest {
    public required string Description { get; set; }
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
}