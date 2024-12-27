using MVVMPrivateClinicProjectDesktopApp.Interfaces;

namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class PrescriptionDto : IEntity {
    public int Id { get; set; }
    public DateOnly DateIssued { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public required string PatientCode { get; set; }
    public required string DoctorCode { get; set; }
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
}