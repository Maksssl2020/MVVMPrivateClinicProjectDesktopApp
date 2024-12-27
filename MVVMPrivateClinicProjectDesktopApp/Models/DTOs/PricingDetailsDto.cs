namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class PricingDetailsDto {
    public int Id { get; set; }
    public required string ServiceName { get; set; }
    public required string ServiceType { get; set; }
    public decimal Price { get; set; }
    public DateOnly EffectiveDate { get; set; }
    public int TotalUses { get; set; }
}