namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class InvoiceDto {
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateIssued { get; set; }
    public DateTime DueDate { get; set; }
    public InvoiceStatus Status { get; set; }
    public required string PatientCode { get; set; }
    public int IdPatient { get; set; }
}