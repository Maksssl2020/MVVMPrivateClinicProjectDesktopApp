namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public interface IEntityStore {
    public Task DeleteEntity(int entityId);
    public int EntityIdToDelete { get; set; }
    public string? EntityCode { get; set; }
}