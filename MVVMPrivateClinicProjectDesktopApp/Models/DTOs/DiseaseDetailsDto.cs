namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class DiseaseDetailsDto {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string DiseaseCode { get; set; }
    public int TotalDiagnoses { get; set; }
}