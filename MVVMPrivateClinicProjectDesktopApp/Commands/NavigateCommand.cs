using MVVMPrivateClinicProjectDesktopApp.Services;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class NavigateCommand(NavigationServiceBase navigationService) : RelayCommand {
    public override void Execute(object? parameter){
        navigationService.Navigate();
    }
}