using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Services;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class ModalNavigationViewModel(
    NavigationServiceBase addNewPatientNavigationService,
    NavigationServiceBase deletePatientNavigationService,
    NavigationServiceBase dataPatientModalNavigationService,
    NavigationServiceBase addNewDiseaseNavigationService,
    NavigationServiceBase addNewMedicineNavigationService,
    NavigationServiceBase addNewDoctorNavigationService,
    NavigationServiceBase addNewPricingNavigationService,
    NavigationServiceBase prescriptionDetailsModalNavigationService,
    NavigationServiceBase patientNoteDetailsModalNavigationService,
    NavigationServiceBase referralDetailsModalNavigationService
    )
    : ViewModelBase {
    public readonly ICommand ShowAddNewPatientModal = new NavigateCommand(addNewPatientNavigationService);
    public readonly ICommand ShowDeletePatientModal = new NavigateCommand(deletePatientNavigationService);
    public readonly ICommand ShowPatientDataModal = new NavigateCommand(dataPatientModalNavigationService);
    public readonly ICommand ShowAddNewDiseaseModal = new NavigateCommand(addNewDiseaseNavigationService);
    public readonly ICommand ShowAddNewMedicineModal = new NavigateCommand(addNewMedicineNavigationService);
    public readonly ICommand ShowAddNewDoctorModal = new NavigateCommand(addNewDoctorNavigationService);
    public readonly ICommand ShowAddNewPricingModal = new NavigateCommand(addNewPricingNavigationService);
    public readonly ICommand ShowPrescriptionDetailsModal = new NavigateCommand(prescriptionDetailsModalNavigationService);
    public readonly ICommand ShowPatientNoteDetailsModal = new NavigateCommand(patientNoteDetailsModalNavigationService);
    public readonly ICommand ShowReferralDetailsModal = new NavigateCommand(referralDetailsModalNavigationService);
}