using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Services;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PatientDataModalNavigationViewModel(
    NavigationServiceBase patientDetailNavigationService,
    NavigationServiceBase prescriptionNavigationService
    ) {
    public ICommand ShowPatientDetailsViewCommand { get; } = new NavigateCommand(patientDetailNavigationService);
    public ICommand ShowIssuePrescriptionViewCommand { get; } = new NavigateCommand(prescriptionNavigationService);
}