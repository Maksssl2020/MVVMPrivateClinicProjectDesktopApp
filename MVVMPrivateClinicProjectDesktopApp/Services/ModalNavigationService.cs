using System.Windows;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;
using MVVMPrivateClinicProjectDesktopApp.Views;

namespace MVVMPrivateClinicProjectDesktopApp.Services;

public class ModalNavigationService(ModalNavigationStore modalNavigationStore, Func<ViewModelBase> createViewModel) : NavigationServiceBase {
    
    public override void Navigate() {
        var viewModel = createViewModel();
        var view = GetModalViewDependsOnViewModel(viewModel);
        modalNavigationStore.OpenModal(viewModel, view);
    }

    private static Window GetModalViewDependsOnViewModel(ViewModelBase viewModel) => viewModel switch {
        AddNewPatientViewModel => new AddNewPatientView(),
        DeletePatientViewModel => new DeletePatientView(),
        PatientDataModalViewModel => new PatientDataModalView(),
        AddNewDiseaseViewModel => new AddNewDiseaseView(),
        AddNewMedicineViewModel => new AddNewMedicineView(),
        AddNewDoctorViewModel => new AddNewDoctorView(),
        AddNewPricingViewModel => new AddNewPricingView(),
        _ => new AddNewPatientView()
    };
}