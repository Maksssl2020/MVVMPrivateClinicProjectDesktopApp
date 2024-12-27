using MVVMPrivateClinicProjectDesktopApp.Services;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class DeleteEntityNavigateCommand(DeleteEntityViewModel viewModel, ModalNavigationStore modalNavigationStore) : RelayCommand {
    public override void Execute(object? parameter){
        if (parameter is not IEntityStore entityStore) return;

        viewModel.EntityStore = entityStore;
        var navigationServiceBase = new ModalNavigationService(modalNavigationStore, () => viewModel);
        navigationServiceBase.Navigate();
    }
}