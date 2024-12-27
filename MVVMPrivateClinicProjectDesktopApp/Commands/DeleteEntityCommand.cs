namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class DeleteEntityCommand(
    int entityIdToDelete,
    Func<int, Task> deleteEntity,
    Action closeModalAction
    ) : AsyncRelayCommand {
    
    public override async Task ExecuteAsync(object? parameter){
        try {
            await deleteEntity.Invoke(entityIdToDelete);
            closeModalAction.Invoke();
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}