using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Services;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PatientDataModalNavigationViewModel(
    NavigationServiceBase patientDetailNavigationService,
    NavigationServiceBase prescriptionNavigationService,
    NavigationServiceBase addNewPatientNoteNavigationService,
    NavigationServiceBase issueReferralNavigationService,
    NavigationServiceBase addDiagnosisNavigationService
    ) {
    public ICommand ShowPatientDetailsViewCommand { get; } = new NavigateCommand(patientDetailNavigationService);
    public ICommand ShowIssuePrescriptionViewCommand { get; } = new NavigateCommand(prescriptionNavigationService);
    public ICommand ShowAddNewPatientNoteViewCommand { get; } = new NavigateCommand(addNewPatientNoteNavigationService);
    public ICommand ShowIssueReferralViewCommand { get; } = new NavigateCommand(issueReferralNavigationService);
    public ICommand ShowAddDiagnosisViewCommand { get; } = new NavigateCommand(addDiagnosisNavigationService);
}