using MVVMPrivateClinicProjectDesktopApp.Interfaces;

namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class PricingDto : IEntity {
    public int Id { get; set; }
    public required string ServiceName { get; set; }
    public required string ServiceType { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
}