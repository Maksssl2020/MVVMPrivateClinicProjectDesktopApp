namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class PatientDetailsDto {
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PatientCode { get; set; }
}