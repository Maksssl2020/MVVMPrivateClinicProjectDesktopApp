using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Services;

public class ParameterNavigationService<TParameter>(
    NavigationStore navigationStore,
    Func<TParameter, ViewModelBase> createViewModel)
    : NavigationServiceBase {
    
    public void Navigate(TParameter parameter){
        navigationStore.CurrentViewModel = createViewModel(parameter);
    }
}