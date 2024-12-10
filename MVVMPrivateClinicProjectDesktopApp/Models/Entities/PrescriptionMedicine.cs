namespace MVVMPrivateClinicProjectDesktopApp.Models.Entities;

public class PrescriptionMedicine {
    public int PrescriptionId { get; set; }
    public virtual Prescription Prescription { get; set; } = null!;

    public int MedicineId { get; set; }
    public virtual Medicine Medicine { get; set; } = null!;
}