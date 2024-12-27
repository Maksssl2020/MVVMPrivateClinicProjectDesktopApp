using MVVMPrivateClinicProjectDesktopApp.Interfaces;

namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class ReferralTestDto : IEntity {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}