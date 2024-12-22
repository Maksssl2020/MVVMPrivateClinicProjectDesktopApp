namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class PatientNoteWithDoctorDataDto {
    public int Id { get; init; }
    public DateTime DateIsuued { get; init; }
    public required string Description { get; init; }
    public required DoctorDto DoctorDto { get; set; }
    public required string PatientCode { get; set; }
    public int IdDoctor { get; init; }
    public int IdPatient { get; set; }
}