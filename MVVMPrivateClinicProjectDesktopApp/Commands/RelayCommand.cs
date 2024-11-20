using System.Windows.Input;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class RelayCommand : ICommand {

    private readonly Action<object> _execute;
    private readonly Predicate<object?>? _canExecute;
    
    public RelayCommand(Action<object> executeMethod){
        _execute = executeMethod;
        _canExecute = null;
    }
    
    public RelayCommand(Action<object> executeMethod, Predicate<object?>? canExecuteMethod){
        _execute = executeMethod;
        _canExecute = canExecuteMethod;
    }
    
    public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

    public void Execute(object? parameter) => _execute(parameter);
    
    public event EventHandler? CanExecuteChanged {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}