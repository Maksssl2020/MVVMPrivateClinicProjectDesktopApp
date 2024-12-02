using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Services;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class ModalNavigationViewModel(
    NavigationServiceBase addNewPatientNavigationService,
    NavigationServiceBase deletePatientNavigationService,
    NavigationServiceBase dataPatientModalNavigationService
    )
    : ViewModelBase {
    public readonly ICommand ShowAddNewPatientModal = new NavigateCommand(addNewPatientNavigationService);
    public readonly ICommand ShowDeletePatientModal = new NavigateCommand(deletePatientNavigationService);
    public readonly ICommand ShowPatientDataModal = new NavigateCommand(dataPatientModalNavigationService);
}