using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadEntityDetailsCommand<T,TV>(EntityStore<T, TV> entityStore) : AsyncRelayCommand where TV : class {
    public override async Task ExecuteAsync(object? parameter){
        try {
            if (parameter is EntityDetailsViewModelBase<TV> viewModel) {
                await entityStore.LoadEntityDetails();
                viewModel.UpdateEntity(entityStore.SelectedEntityDetails);
            }
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}