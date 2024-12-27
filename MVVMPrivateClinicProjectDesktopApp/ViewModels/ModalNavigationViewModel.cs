using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Services;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class ModalNavigationViewModel(
    ModalNavigationStore modalNavigationStore,
    NavigationServiceBase addNewPatientNavigationService,
    NavigationServiceBase dataPatientModalNavigationService,
    NavigationServiceBase addNewDiseaseNavigationService,
    NavigationServiceBase addNewMedicineNavigationService,
    NavigationServiceBase addNewDoctorNavigationService,
    NavigationServiceBase addNewPricingNavigationService,
    NavigationServiceBase prescriptionDetailsModalNavigationService,
    NavigationServiceBase patientNoteDetailsModalNavigationService,
    NavigationServiceBase referralDetailsModalNavigationService,
    NavigationServiceBase addNewAppointmentModalNavigationService,
    NavigationServiceBase selectPatientToAddSpecificDataModalNavigationService,
    NavigationServiceBase addNewInvoiceModalNavigationService,
    NavigationServiceBase invoiceDetailsModalNavigationService,
    NavigationServiceBase doctorDetailsModalNavigationService,
    NavigationServiceBase medicineDetailsModalNavigationService,
    NavigationServiceBase diseaseDetailsModalNavigationService,
    NavigationServiceBase referralTestDetailsModalNavigationService,
    NavigationServiceBase pricingDetailsModalNavigationService,
    NavigationServiceBase appointmentDetailsModalNavigationService,
    NavigationServiceBase addNewReferralTestModalNavigationService
    )
    : ViewModelBase {
    public readonly ICommand ShowAddNewPatientModal = new NavigateCommand(addNewPatientNavigationService);
    public readonly ICommand ShowDeleteEntityModal = new DeleteEntityNavigateCommand(new DeleteEntityViewModel(), modalNavigationStore);
    public readonly ICommand ShowPatientDataModal = new NavigateCommand(dataPatientModalNavigationService);
    public readonly ICommand ShowAddNewDiseaseModal = new NavigateCommand(addNewDiseaseNavigationService);
    public readonly ICommand ShowAddNewMedicineModal = new NavigateCommand(addNewMedicineNavigationService);
    public readonly ICommand ShowAddNewDoctorModal = new NavigateCommand(addNewDoctorNavigationService);
    public readonly ICommand ShowAddNewPricingModal = new NavigateCommand(addNewPricingNavigationService);
    public readonly ICommand ShowPrescriptionDetailsModal = new NavigateCommand(prescriptionDetailsModalNavigationService);
    public readonly ICommand ShowPatientNoteDetailsModal = new NavigateCommand(patientNoteDetailsModalNavigationService);
    public readonly ICommand ShowReferralDetailsModal = new NavigateCommand(referralDetailsModalNavigationService);
    public readonly ICommand ShowAddNewAppointmentModal = new NavigateCommand(addNewAppointmentModalNavigationService);
    public readonly ICommand ShowSelectPatientToAddSpecificDataModal = new NavigateCommand(selectPatientToAddSpecificDataModalNavigationService);
    public readonly ICommand ShowAddNewInvoiceModal = new NavigateCommand(addNewInvoiceModalNavigationService);
    public readonly ICommand ShowInvoiceDetailsModal = new NavigateCommand(invoiceDetailsModalNavigationService);
    public readonly ICommand ShowDoctorDetailsModal = new NavigateCommand(doctorDetailsModalNavigationService);
    public readonly ICommand ShowMedicineDetailsModal = new NavigateCommand(medicineDetailsModalNavigationService);
    public readonly ICommand ShowDiseaseDetailsModal = new NavigateCommand(diseaseDetailsModalNavigationService);
    public readonly ICommand ShowReferralTestDetailsModal = new NavigateCommand(referralTestDetailsModalNavigationService);
    public readonly ICommand ShowPricingDetailsModal = new NavigateCommand(pricingDetailsModalNavigationService);
    public readonly ICommand ShowAppointmentDetailsModal = new NavigateCommand(appointmentDetailsModalNavigationService);
    public readonly ICommand ShowAddNewReferralTestModal = new NavigateCommand(addNewReferralTestModalNavigationService);
}