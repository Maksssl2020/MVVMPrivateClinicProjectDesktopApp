namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class DoctorDto {
    public int Id { get; set; }
    public string DoctorCode { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? DoctorSpecialization { get; set; }
    public int IdDoctorCard { get; set; }
}