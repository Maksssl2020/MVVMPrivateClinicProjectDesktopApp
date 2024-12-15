namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class DoctorDtoBase {
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string DoctorSpecialization { get; set; }
    public int IdDoctorSpecialization { get; set; }

}