using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class NavigateCommand<T>(Action execute) : RelayCommand
    where T : ViewModelBase {
    public override void Execute(object? parameter){
        execute();
    }
}