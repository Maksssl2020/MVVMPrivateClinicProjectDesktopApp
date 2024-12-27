using MVVMPrivateClinicProjectDesktopApp.Interfaces;

namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class AppointmentDto : IEntity {
    public int Id { get; set; }
    public required string AppointmentStatus { get; set; }
    public required DoctorDtoBase DoctorDtoBase { get; set; }
    public required PatientDetailsDto PatientDetailsDto { get; set; }
    public required PricingDto PricingDto { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int IdDoctor { get; set; }
    public int IdPatient { get; set; }
    public int IdPricing { get; set; }
}