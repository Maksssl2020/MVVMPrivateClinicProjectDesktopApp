using System.Windows.Input;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public abstract class RelayCommand : ICommand {
    
    public event EventHandler? CanExecuteChanged;
    
    public virtual bool CanExecute(object? parameter) => true;
    public abstract void Execute(object? parameter);

    public void OnCanExecuteChanged() {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}