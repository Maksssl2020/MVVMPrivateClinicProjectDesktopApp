using MVVMPrivateClinicProjectDesktopApp.Services;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class ParameterNavigateCommand<TParameter>(
    ParameterNavigationService<TParameter> navigationService,
    TParameter tParameter
) : RelayCommand {

    public override void Execute(object? parameter){
        navigationService.Navigate(tParameter);
    }
}