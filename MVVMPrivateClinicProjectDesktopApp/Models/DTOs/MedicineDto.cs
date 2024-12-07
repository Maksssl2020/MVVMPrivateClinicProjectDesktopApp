namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class MedicineDto {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public required string Description { get; set; }
}