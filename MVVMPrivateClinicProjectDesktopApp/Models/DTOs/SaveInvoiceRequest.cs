namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class SaveInvoiceRequest {
    public decimal Amount { get; set; }
    public int IdPatient { get; set; }
    public int IdPricing { get; set; }
}