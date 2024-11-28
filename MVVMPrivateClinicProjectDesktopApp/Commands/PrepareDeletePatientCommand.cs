namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class PrepareDeletePatientCommand<T>(Action<T> execute) : RelayCommand{
    public override void Execute(object? parameter){
        if (parameter == null) return; 
        execute((T)parameter);
    }
}