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
        DeleteEntityViewModel => new DeleteEntityView(),
        PatientDataModalViewModel => new PatientDataModalView(),
        AddNewDiseaseViewModel => new AddNewDiseaseView(),
        AddNewMedicineViewModel => new AddNewMedicineView(),
        AddNewDoctorViewModel => new AddNewDoctorView(),
        AddNewPricingViewModel => new AddNewPricingView(),
        PrescriptionDetailsViewModel => new PrescriptionDetailsModalView(),
        PatientNoteDetailsViewModel => new PatientNoteDetailsView(),
        ReferralDetailsViewModel => new ReferralDetailsView(),
        AddNewAppointmentViewModel => new AddNewAppointmentView(),
        SelectPatientToAddSpecificDataViewModel => new SelectPatientToAddSpecificDataView(),
        AddNewInvoiceViewModel => new AddNewInvoiceView(),
        InvoiceDetailsViewModel => new InvoiceDetailsModalView(),
        DoctorDetailsViewModel => new DoctorDetailsView(),
        MedicineDetailsViewModel => new MedicineDetailsView(),
        DiseaseDetailsViewModel => new DiseaseDetailsView(),
        ReferralTestDetailsViewModel => new ReferralTestDetailsView(),
        PricingDetailsViewModel => new PricingDetailsView(),
        AppointmentDetailsViewModel => new AppointmentDetailsView(),
        AddNewReferralTestViewModel => new AddNewReferralTestView(),
        _ => new AddNewPatientView()
    };
}