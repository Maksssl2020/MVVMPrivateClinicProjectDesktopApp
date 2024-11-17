namespace MVVMPrivateClinicProjectDesktopApp.Models.Entities;

public partial class Address
{
    public int Id { get; set; }

    public string? PostalCode { get; set; }

    public string? Street { get; set; }

    public string? City { get; set; }

    public string? BuildingNumber { get; set; }

    public string? LocalNumber { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
