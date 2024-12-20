namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class InvoiceDetailsDto {
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateIssued { get; set; }
    public DateTime DueDate { get; set; }
    public InvoiceStatus Status { get; set; }
    public PatientDetailsDto PatientDetailsDto { get; set; } = null!;
    public PricingDto PricingDto { get; set; } = null!;
    public int IdPatient { get; set; }
    public int IdPricing { get; set; }
}