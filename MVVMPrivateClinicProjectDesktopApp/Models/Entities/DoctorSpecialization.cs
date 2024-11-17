namespace MVVMPrivateClinicProjectDesktopApp.Models.Entities;

public partial class DoctorSpecialization
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
