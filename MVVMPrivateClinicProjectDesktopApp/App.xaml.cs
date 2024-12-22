using System.Windows;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Services;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;
using MVVMPrivateClinicProjectDesktopApp.Views;
using NavigationService = MVVMPrivateClinicProjectDesktopApp.Services.NavigationService;

namespace MVVMPrivateClinicProjectDesktopApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application {

    private const string ConnectionString = "Server=localhost;Database=PrivateClinic;Trusted_Connection=True;TrustServerCertificate=True";
    
    private readonly ServiceProvider _serviceProvider;
    private readonly NavigationStore _navigationStore;
    private readonly ModalNavigationStore _modalNavigationStore;
    private readonly PatientStore _patientStore;
    private readonly AppointmentStore _appointmentStore;
    private readonly MedicineStore _medicineStore;
    private readonly DoctorStore _doctorStore;
    private readonly PatientDataModalNavigationStore _patientDataModalNavigationStore;
    private readonly PrescriptionStore _prescriptionStore;
    private readonly ReferralStore _referralStore;
    private readonly InvoiceStore _invoiceStore;
    private readonly PricingStore _pricingStore;
    private readonly PatientNoteStore _patientNoteStore;
    private readonly DiseaseStore _diseaseStore;
    private readonly DoctorSpecializationStore _doctorSpecializationStore;
    private readonly ReferralTestStore _referralTestStore;
    private readonly DiagnosisStore _diagnosisStore;
    private readonly AppointmentDateStore _appointmentDateStore;
    private readonly AddSpecificDataToPatientStore _addSpecificDataToPatientStore;
    private readonly StatisticsStore _statisticsStore;
    
    private readonly NavigationBarViewModel _navigationBarViewModel;
    private readonly ModalNavigationViewModel _modalNavigationViewModel;
    private readonly PatientDataModalNavigationViewModel _patientDataModalNavigationViewModel;
    
    public App(){
        IServiceCollection services = new ServiceCollection();
        services.AddAutoMapper(typeof(App));
        var mapper = services.BuildServiceProvider().GetService<IMapper>();
        MyMapper.Mapper = mapper!;
        _serviceProvider = services.BuildServiceProvider();

        IUnitOfWork unitOfWork = new UnitOfWork.UnitOfWork(new DbContextFactory(ConnectionString));
        _navigationStore = new NavigationStore();
        _patientStore = new PatientStore(unitOfWork);
        _modalNavigationStore = new ModalNavigationStore();
        _appointmentStore = new AppointmentStore(unitOfWork);
        _patientDataModalNavigationStore = new PatientDataModalNavigationStore();
        _medicineStore = new MedicineStore(unitOfWork);
        _doctorStore = new DoctorStore(unitOfWork);
        _prescriptionStore = new PrescriptionStore(unitOfWork);
        _referralStore = new ReferralStore(unitOfWork);
        _invoiceStore = new InvoiceStore(unitOfWork);
        _pricingStore = new PricingStore(unitOfWork);
        _patientNoteStore = new PatientNoteStore(unitOfWork);
        _diseaseStore = new DiseaseStore(unitOfWork);
        _doctorSpecializationStore = new DoctorSpecializationStore(unitOfWork);
        _referralTestStore = new ReferralTestStore(unitOfWork);
        _diagnosisStore = new DiagnosisStore(unitOfWork);
        _appointmentDateStore = new AppointmentDateStore(unitOfWork);
        _addSpecificDataToPatientStore = new AddSpecificDataToPatientStore();
        _statisticsStore = new StatisticsStore(unitOfWork);
        
        _navigationBarViewModel = new NavigationBarViewModel(
            CreateHomeNavigationService(),
            CreatePatientsNavigationService(),
            CreateDoctorsNavigationService(),
            CreateAppointmentsNavigationService(),
            CreateDiseasesNavigationService(),
            CreateMedicinesNavigationService(),
            CreatePrescriptionsNavigationService(),
            CreateReferralsNavigationService(),
            CreateInvoicesNavigationService(),
            CreatePricingNavigationService(),
            CreatePatientsNotesNavigationService(),
            CreateReferralTestsNavigationService()
        );
        _modalNavigationViewModel = new ModalNavigationViewModel(
            CreateAddNewPatientsNavigationService(),
            CreateDeletePatientsNavigationService(),
            CreatePatientDataModalNavigationService(),
            CreateAddNewDiseaseNavigationService(),
            CreateAddNewMedicineNavigationService(),
            CreateAddNewDoctorNavigationService(),
            CreateAddNewPricingNavigationService(),
            CreatePrescriptionDetailsNavigationService(),
            CreatePatientNoteDetailsNavigationService(),
            CreateReferralDetailsNavigationService(),
            CreateAddNewAppointmentNavigationService(),
            CreateSelectPatientToAddSpecificDataNavigationService(),
            CreateAddNewInvoiceNavigationService(),
            CreateInvoiceDetailsNavigationService(),
            CreateDoctorDetailsNavigationService(),
            CreateMedicineDetailsNavigationService(),
            CreateDiseaseDetailsNavigationStore(),
            CreateReferralTestDetailsNavigationStore()
            );

        _patientDataModalNavigationViewModel = new PatientDataModalNavigationViewModel(
            CreatePatientDetailsNavigationService(),
            CreateIssuePrescriptionsNavigationService(),
            CreateAddNewPatientNoteNavigationService(),
            CreateIssueReferralNavigationService(),
            CreateAddDiagnosisNavigationService()
        );
    }
    
    protected override void OnStartup(StartupEventArgs e){
        var homeNavigationService = CreateHomeNavigationService();
        homeNavigationService.Navigate();
        
        var windowView = new MainWindowView {
            DataContext = new MainWindowViewModel(_navigationStore, _navigationBarViewModel)
        };
        windowView.Show();
        
        base.OnStartup(e);
    }

    private NavigationServiceBase CreateHomeNavigationService(){
        return new NavigationService(_navigationStore, () => HomeViewModel.LoadHomeViewModel(_doctorStore, _pricingStore, _appointmentStore, _statisticsStore));
    }
    
    private NavigationServiceBase CreatePatientsNavigationService(){
        return new NavigationService(_navigationStore, () => PatientsViewModel.LoadPatientsViewModel(_patientStore, _modalNavigationViewModel));
    }
    
    private NavigationServiceBase CreateDoctorsNavigationService(){
        return new NavigationService(_navigationStore, () => DoctorsViewModel.LoadDoctorsViewModel(_doctorStore, _modalNavigationViewModel));
    }

    private NavigationService CreateAppointmentsNavigationService(){
        return new NavigationService(_navigationStore, () => AppointmentsViewModel.LoadAppointmentsViewModel(_appointmentStore, _modalNavigationViewModel));
    }
    
    private NavigationServiceBase CreateDiseasesNavigationService(){
        return new NavigationService(_navigationStore, () => DiseasesViewModel.LoadDiseasesViewModel(_diseaseStore, _modalNavigationViewModel));
    }
    
    private NavigationServiceBase CreateMedicinesNavigationService(){
        return new NavigationService(_navigationStore, () => MedicinesViewModel.LoadMedicinesViewModel(_medicineStore, _modalNavigationViewModel));
    }

    private NavigationServiceBase CreatePrescriptionsNavigationService(){
        return new NavigationService(_navigationStore, () => PrescriptionsViewModel.LoadPrescriptionsViewModel(_prescriptionStore, _addSpecificDataToPatientStore, _modalNavigationViewModel));
    }

    private NavigationServiceBase CreateReferralsNavigationService(){
        return new NavigationService(_navigationStore, () => ReferralsViewModel.LoadReferralsViewModel(_referralStore, _addSpecificDataToPatientStore, _modalNavigationViewModel));
    }

    private NavigationServiceBase CreateInvoicesNavigationService(){
        return new NavigationService(_navigationStore, () => InvoicesViewModel.LoadInvoicesViewModel(_invoiceStore, _modalNavigationViewModel));
    }

    private NavigationServiceBase CreatePricingNavigationService(){
        return new NavigationService(_navigationStore, () => PricingViewModel.LoadPricingViewModel(_pricingStore, _modalNavigationViewModel));
    }

    private NavigationServiceBase CreatePatientsNotesNavigationService(){
        return new NavigationService(_navigationStore, () => PatientsNotesViewModel.LoadPatientNoteViewModel(_patientNoteStore, _addSpecificDataToPatientStore, _modalNavigationViewModel));
    }

    private NavigationServiceBase CreateReferralTestsNavigationService(){
        return new NavigationService(_navigationStore, () => ReferralTestsViewModel.LoadReferralTestsViewModel(_referralTestStore, _modalNavigationViewModel));
    }
    
    private NavigationServiceBase CreateAddNewPatientsNavigationService(){
        return new ModalNavigationService(_modalNavigationStore, () => new AddNewPatientViewModel(_patientStore));
    }

    private NavigationServiceBase CreateDeletePatientsNavigationService(){
        return new ModalNavigationService(_modalNavigationStore, () => new DeletePatientViewModel(_patientStore));
    }

    private NavigationServiceBase CreatePatientDataModalNavigationService(){
        return new ModalNavigationService(_modalNavigationStore, () =>  PatientDataModalViewModel.LoadPatientDataModalViewModel(_patientDataModalNavigationStore, _patientDataModalNavigationViewModel, _patientStore));
    }
    
    private NavigationServiceBase CreateAddNewDiseaseNavigationService(){
        return new ModalNavigationService(_modalNavigationStore, () => new AddNewDiseaseViewModel(_diseaseStore));
    }

    private NavigationServiceBase CreateAddNewMedicineNavigationService(){
        return new ModalNavigationService(_modalNavigationStore, () => AddNewMedicineViewModel.LoadAddNewMedicineViewModel(_medicineStore));
    }

    private NavigationServiceBase CreateAddNewDoctorNavigationService(){
        return new ModalNavigationService(_modalNavigationStore, () => AddNewDoctorViewModel.LoadAddNewDoctorViewModel(_doctorStore, _doctorSpecializationStore));
    }

    private NavigationServiceBase CreateAddNewPricingNavigationService(){
        return new ModalNavigationService(_modalNavigationStore, () => AddNewPricingViewModel.LoadAddNewPricingViewModel(_pricingStore));
    }

    private NavigationServiceBase CreatePrescriptionDetailsNavigationService(){
        return new ModalNavigationService(_modalNavigationStore, () =>  PrescriptionDetailsViewModel.LoadPrescriptionDetailsViewModel(_prescriptionStore));
    }

    private NavigationServiceBase CreateAddNewInvoiceNavigationService(){
        return new ModalNavigationService(_modalNavigationStore,
            () => AddNewInvoiceViewModel.LoadAddNewInvoiceViewModel(_invoiceStore, _patientStore, _pricingStore));
    }
    
    private NavigationServiceBase CreatePatientNoteDetailsNavigationService(){
        return new ModalNavigationService(_modalNavigationStore,
            () => PatientNoteDetailsViewModel.LoadPatientNoteDetailsViewModel(_patientNoteStore));
    }

    private NavigationServiceBase CreateReferralDetailsNavigationService(){
        return new ModalNavigationService(_modalNavigationStore, () =>  ReferralDetailsViewModel.LoadReferralDetailsViewModel(_referralStore));
    }

    private NavigationServiceBase CreateAddNewAppointmentNavigationService(){
        return new ModalNavigationService(_modalNavigationStore, () =>  AddNewAppointmentViewModel.LoadAddNewAppointmentViewModel(_doctorStore, _patientStore, _appointmentStore, _appointmentDateStore, _pricingStore, _invoiceStore));
    }

    private NavigationServiceBase CreateSelectPatientToAddSpecificDataNavigationService(){
        return new ModalNavigationService(_modalNavigationStore,
            () => SelectPatientToAddSpecificDataViewModel.LoadSelectPatientToAddSpecificDataViewModel(_patientStore,
                _addSpecificDataToPatientStore, _modalNavigationViewModel));
    }

    private NavigationServiceBase CreateInvoiceDetailsNavigationService(){
        return new ModalNavigationService(_modalNavigationStore,
            () => InvoiceDetailsViewModel.LoadInvoiceDetailsViewModel(_invoiceStore));
    }

    private NavigationServiceBase CreateDoctorDetailsNavigationService(){
        return new ModalNavigationService(_modalNavigationStore,
            () => DoctorDetailsViewModel.LoadDoctorDetailsViewModel(_doctorStore, _appointmentStore, _patientNoteStore,
                _prescriptionStore, _referralStore, _diagnosisStore));
    }

    private NavigationServiceBase CreateMedicineDetailsNavigationService(){
        return new ModalNavigationService(_modalNavigationStore,
            () => MedicineDetailsViewModel.LoadMedicineDetailsViewModel(_medicineStore));
    }

    private NavigationServiceBase CreateDiseaseDetailsNavigationStore(){
        return new ModalNavigationService(_modalNavigationStore,
            () => DiseaseDetailsViewModel.LoadDiseaseDetailsViewModel(_diseaseStore));
    }

    private NavigationServiceBase CreateReferralTestDetailsNavigationStore(){
        return new ModalNavigationService(_modalNavigationStore,
            () => ReferralTestDetailsViewModel.LoadReferralTestDetailsViewModel(_referralTestStore));
    }
    
    private NavigationServiceBase CreatePatientDetailsNavigationService(){
        return new PatientDataModalNavigationService(_patientDataModalNavigationStore, () => PatientDetailsViewModel.LoadPatientDetailsViewModel(_patientStore, _appointmentStore, _prescriptionStore, _referralStore, _diagnosisStore));
    }
    
    private NavigationServiceBase CreateIssuePrescriptionsNavigationService(){
        return new PatientDataModalNavigationService(_patientDataModalNavigationStore, () => IssuePrescriptionViewModel.LoadIssuePrescriptionViewModel(_medicineStore, _doctorStore, _patientStore, _prescriptionStore));
    }

    private NavigationServiceBase CreateAddNewPatientNoteNavigationService(){
        return new PatientDataModalNavigationService(_patientDataModalNavigationStore,
            () => AddNewPatientNoteViewModel.LoadAddNewPatientNoteViewModel(_doctorStore, _patientStore,
                _patientNoteStore));
    }

    private NavigationServiceBase CreateIssueReferralNavigationService(){
        return new PatientDataModalNavigationService(_patientDataModalNavigationStore, () => IssueReferralViewModel.LoadIssueReferralViewModel(_patientStore, _referralStore, _doctorStore, _diseaseStore, _referralTestStore));
    }

    private NavigationServiceBase CreateAddDiagnosisNavigationService(){
        return new PatientDataModalNavigationService(_patientDataModalNavigationStore,
            () => AddNewDiagnosisViewModel.LoadAddDiagnosisViewModel(_patientStore, _diagnosisStore, _doctorStore,
                _diseaseStore));
    }
}