using MVVMPrivateClinicProjectDesktopApp.Interfaces;

namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class DiseaseDto : IEntity {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string DiseaseCode { get; set; }
}