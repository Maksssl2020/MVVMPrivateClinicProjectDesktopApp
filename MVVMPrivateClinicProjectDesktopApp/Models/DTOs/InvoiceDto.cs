using MVVMPrivateClinicProjectDesktopApp.Interfaces;

namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class InvoiceDto : IEntity {
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public required string Status { get; set; }
    public DateTime DateIssued { get; set; }
    public DateTime DueDate { get; set; }
    public required string PatientCode { get; set; }
    public int IdPatient { get; set; }
}