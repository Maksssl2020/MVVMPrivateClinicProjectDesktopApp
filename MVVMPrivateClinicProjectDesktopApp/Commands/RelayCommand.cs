using System.Windows.Input;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public abstract class RelayCommand : ICommand {
    
    public virtual bool CanExecute(object? parameter){
        return true;
    }
    public abstract void Execute(object? parameter);

    public event EventHandler? CanExecuteChanged;
}