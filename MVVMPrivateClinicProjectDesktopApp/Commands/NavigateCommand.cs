using MVVMPrivateClinicProjectDesktopApp.Services;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class NavigateCommand(NavigationService navigationService) : RelayCommand {
    public override void Execute(object? parameter){
        navigationService.Navigate();
    }
}