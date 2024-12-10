namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class DoctorFullNameAndSpecializationDto {
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Specialization { get; set; }
}