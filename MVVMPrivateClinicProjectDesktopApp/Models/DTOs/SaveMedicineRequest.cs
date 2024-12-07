namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class SaveMedicineRequest {
    public required string Name { get; set; }
    public required string Type { get; set; }
    public required string Description { get; set; }
}