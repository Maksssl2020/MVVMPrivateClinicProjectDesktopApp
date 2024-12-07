namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class SavePricingRequest {
    public required string ServiceName { get; set; }
    public required string ServiceType { get; set; }
    public decimal Price { get; set; }
}