using MVVMPrivateClinicProjectDesktopApp.Interfaces;

namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class MedicineDto : IEntity {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public required string Description { get; set; }
}