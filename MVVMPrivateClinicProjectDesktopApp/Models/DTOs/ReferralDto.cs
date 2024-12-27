using MVVMPrivateClinicProjectDesktopApp.Interfaces;

namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class ReferralDto : IEntity {
    public int Id { get; set; }
    public DateTime DateIssued { get; set; }
    public required string Name { get; set; }
    public required string PatientCode { get; set; }
    public required string DoctorCode { get; set; }
    public required string DiseaseCode { get; set; }
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
    public int IdDisease { get; set; }
}