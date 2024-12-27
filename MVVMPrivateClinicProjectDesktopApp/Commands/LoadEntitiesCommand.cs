using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadEntitiesCommand<T, TV>(Action<IEnumerable<T>> updateEntities, EntityStore<T, TV> loadEntities) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await loadEntities.LoadEntities();
            updateEntities.Invoke(loadEntities.EntitiesCollection);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}