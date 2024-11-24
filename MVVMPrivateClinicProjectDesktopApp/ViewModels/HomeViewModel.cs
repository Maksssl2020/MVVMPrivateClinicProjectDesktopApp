namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class HomeViewModel : ViewModelBase {
    private bool _isHomeCurrentView;

    public bool IsHomeCurrentView {
        get => _isHomeCurrentView;
        set { _isHomeCurrentView = value; OnPropertyChanged();}
    }
    
    
}
    
