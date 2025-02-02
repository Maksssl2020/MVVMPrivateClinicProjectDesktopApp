namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class MedicineDetailsDto {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public int TotalUses { get; set; }
    public required string Description { get; set; }
}