namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class ReferralTestDetailsDto {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int TotalUses { get; set; }
}