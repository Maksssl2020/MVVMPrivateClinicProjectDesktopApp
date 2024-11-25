namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class SubmitCommand(Action execute, Func<bool> canExecute) : RelayCommand {
    public override void Execute(object? parameter){
        execute();
    }
    
    public override bool CanExecute(object? parameter){
        return canExecute();
    }
}