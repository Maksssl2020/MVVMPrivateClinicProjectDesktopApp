using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class RestoreDisabledDataCommand(DisabledDataStore disabledDataStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            if (parameter is int dataId) {
                await disabledDataStore.DeleteEntity(dataId);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}