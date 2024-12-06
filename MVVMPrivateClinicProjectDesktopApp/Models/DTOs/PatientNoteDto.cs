namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class PatientNoteDto {
    public int Id { get; set; }
    public DateTime DateIssued { get; set; }
    public required string Description { get; set; }
    public required string PatientCode { get; set; }
    public required string DoctorCode { get; set; }
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
}