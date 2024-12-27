namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Base;

public interface IBaseRepository<T> {
    public Task<IEnumerable<T>> GetAllEntitiesAsync();
    public Task<T?> GetEntityByIdAsync(int entityId);
    public Task DeleteEntityAsync(int entityId);
    Task<int> CountEntitiesAsync();
}