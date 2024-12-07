namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class PatientNoteWithDoctorDataDto {
    public int Id { get; init; }
    public DateTime DateIssued { get; init; }
    public required string Description { get; init; }
    public required string DoctorFirstName { get; set; }
    public required string DoctorLastName { get; set; }
    public required string DoctorSpecialization { get; set; }
    public required string DoctorCode { get; set; }
    public int IdDoctor { get; init; }
}