namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class DoctorDto : DoctorDtoBase {
    public string DoctorCode { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public int IdDoctorCard { get; set; }
    public int AmountOfAppointments { get; set; }
}