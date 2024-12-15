namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class AppointmentDto {
    public int Id { get; set; }
    public required string AppointmentStatus { get; set; }
    public required DoctorDetailsDto DoctorDetailsDto { get; set; }
    public required PatientDetailsDto PatientDetailsDto { get; set; }
    public required PricingDto PricingDto { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int IdDoctor { get; set; }
    public int IdPatient { get; set; }
    public int IdPricing { get; set; }
}