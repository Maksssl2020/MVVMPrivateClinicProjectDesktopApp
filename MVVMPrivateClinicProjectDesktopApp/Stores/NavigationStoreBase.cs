using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public abstract class NavigationStoreBase<T> {
    public event Action? CurrentViewModelChanged;
    public T? CurrentViewModel { get; set; }
    
    protected void OnCurrentViewModelChanged(){
        CurrentViewModelChanged?.Invoke();
    }
}