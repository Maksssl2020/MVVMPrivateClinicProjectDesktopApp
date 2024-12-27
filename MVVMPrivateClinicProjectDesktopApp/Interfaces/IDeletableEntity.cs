namespace MVVMPrivateClinicProjectDesktopApp.Interfaces;

public interface IDeletableEntity {
    public int Id { get; set; }
    public bool? IsDeleted { get; set; }
    public DateTime? DisabledDate { get; set; }
}