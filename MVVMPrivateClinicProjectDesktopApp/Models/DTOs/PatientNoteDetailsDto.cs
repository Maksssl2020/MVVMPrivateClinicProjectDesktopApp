namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class PatientNoteDetailsDto {
    public int Id { get; set; }
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
    public required string Description { get; set; }
    public DateOnly DateIssued { get; set; }
    public PatientDetailsDto PatientDetailsDto { get; set; } = null!;
    public DoctorDetailsDto DoctorDetailsDto { get; set; } = null!;
}
