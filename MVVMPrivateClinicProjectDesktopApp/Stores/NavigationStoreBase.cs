using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public abstract class NavigationStoreBase {
    public event Action? CurrentViewModelChanged;
    public ViewModelBase? CurrentViewModel { get; set; }
    
    protected void OnCurrentViewModelChanged(){
        CurrentViewModelChanged?.Invoke();
    }
}