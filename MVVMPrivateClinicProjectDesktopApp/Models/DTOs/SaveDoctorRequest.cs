namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class SaveDoctorRequest {
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
    public int DoctorSpecializationId { get; set; }
}