using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Services;

public class PatientDataModalNavigationService(PatientDataModalNavigationStore patientDataModalNavigationStore, Func<ViewModelBase> createViewModel) : NavigationServiceBase {
    public override void Navigate(){
        var viewModel = createViewModel();  
        patientDataModalNavigationStore.ChangeCurrentViewModel(viewModel);

        Console.WriteLine("CHANGING!");
    }
}