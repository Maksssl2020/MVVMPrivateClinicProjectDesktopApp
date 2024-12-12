namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class PatientDto {
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PatientCode { get; set; }
    public string? EmailAddress { get; set; }
    public string? PhoneNumber { get; set; }
    public int IdAddress { get; set; }
}