namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class AppointmentDto {
    public int Id { get; set; }
    public required string DoctorFirstName { get; set; }
    public required string DoctorLastName { get; set; }
    public required string DoctorSpecialization { get; set; }
    public required string PatientFirstName { get; set; }
    public required string PatientLastName { get; set; }
    public required string PatientCode { get; set; }
    public required string AppointmentStatus { get; set; }
    public DateTime AppointmentDate { get; set; }
}