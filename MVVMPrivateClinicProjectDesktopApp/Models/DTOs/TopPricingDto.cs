namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class TopPricingDto {
    public int Position { get; set; }
    public required string ServiceName { get; set; }
    public decimal Price { get; set; }
    public int TotalUseAmount { get; set; }
}