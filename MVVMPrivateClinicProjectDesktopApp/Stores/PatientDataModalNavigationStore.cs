using System.Windows;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class PatientDataModalNavigationStore : NavigationStoreBase {
    public void ChangeCurrentViewModel(ViewModelBase viewMode){
        CurrentViewModel?.Dispose();
        CurrentViewModel = viewMode;
        
        OnCurrentViewModelChanged();
    }
}