namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class SaveAddressRequest {
    public required string City { get; set; }
    public required string PostalCode { get; set; }
    public required string Street { get; set; }
    public required string BuildingNumber { get; set; }
    public string? LocalNumber { get; set; }
}