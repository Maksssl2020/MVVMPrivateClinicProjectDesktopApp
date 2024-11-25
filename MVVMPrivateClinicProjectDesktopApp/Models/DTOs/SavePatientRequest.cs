namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class SavePatientRequest {
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public required int AddressId { get; set; }
}