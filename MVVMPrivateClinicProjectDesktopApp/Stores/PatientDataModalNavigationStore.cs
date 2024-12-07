using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class PatientDataModalNavigationStore : NavigationStoreBase<ViewModelBase> {
    public int SelectedPatientId { get; set; }
    
    public void ChangeCurrentViewModel(ViewModelBase viewMode){
        CurrentViewModel?.Dispose();
        CurrentViewModel = viewMode;
        
        OnCurrentViewModelChanged();
    }
}