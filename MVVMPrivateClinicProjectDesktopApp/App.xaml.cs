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
    private readonly IUnitOfWork _unitOfWork;
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
    private readonly NavigationBarViewModel _navigationBarViewModel;
    private readonly ModalNavigationViewModel _modalNavigationViewModel;
    private readonly PatientDataModalNavigationViewModel _patientDataModalNavigationViewModel;
    
    public App(){
        IServiceCollection services = new ServiceCollection();
        services.AddAutoMapper(typeof(App));
        var mapper = services.BuildServiceProvider().GetService<IMapper>();
        MyMapper.Mapper = mapper!;
        _serviceProvider = services.BuildServiceProvider();

        _unitOfWork = new UnitOfWork.UnitOfWork(new DbContextFactory(ConnectionString));
        _navigationStore = new NavigationStore();
        _patientStore = new PatientStore(_unitOfWork);
        _modalNavigationStore = new ModalNavigationStore();
        _appointmentStore = new AppointmentStore(_unitOfWork);
        _patientDataModalNavigationStore = new PatientDataModalNavigationStore();
        _medicineStore = new MedicineStore(_unitOfWork);
        _doctorStore = new DoctorStore(_unitOfWork);
        _prescriptionStore = new PrescriptionStore(_unitOfWork);
        _referralStore = new ReferralStore(_unitOfWork);
        _invoiceStore = new InvoiceStore(_unitOfWork);
        _pricingStore = new PricingStore(_unitOfWork);
        _patientNoteStore = new PatientNoteStore(_unitOfWork);
        _diseaseStore = new DiseaseStore(_unitOfWork);
        _doctorSpecializationStore = new DoctorSpecializationStore(_unitOfWork);
        _referralTestStore = new ReferralTestStore(_unitOfWork);
        
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
            CreatePatientsNotesNavigationService()
        );
        _modalNavigationViewModel = new ModalNavigationViewModel(
            CreateAddNewPatientsNavigationService(),
            CreateDeletePatientsNavigationService(),
            CreatePatientDataModalNavigationService(),
            CreateAddNewDiseaseNavigationService(),
            CreateAddNewMedicineNavigationService(),
            CreateAddNewDoctorNavigationService(),
            CreateAddNewPricingNavigationService()
            );

        _patientDataModalNavigationViewModel = new PatientDataModalNavigationViewModel(
            CreatePatientDetailsNavigationService(),
            CreateIssuePrescriptionsNavigationService(),
            CreateAddNewPatientNoteNavigationService(),
            CreateIssueReferralNavigationService()
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
        return new NavigationService(_navigationStore, () => new HomeViewModel());
    }
    
    private NavigationServiceBase CreatePatientsNavigationService(){
        return new NavigationService(_navigationStore, () => PatientsViewModel.LoadPatientsViewModel(_patientStore, _modalNavigationViewModel));
    }
    
    private NavigationServiceBase CreateDoctorsNavigationService(){
        return new NavigationService(_navigationStore, () => DoctorsViewModel.LoadDoctorsViewModel(_doctorStore, _modalNavigationViewModel));
    }

    private NavigationService CreateAppointmentsNavigationService(){
        return new NavigationService(_navigationStore, () => AppointmentsViewModel.LoadAppointmentsViewModel(_appointmentStore));
    }
    
    private NavigationServiceBase CreateDiseasesNavigationService(){
        return new NavigationService(_navigationStore, () => DiseasesViewModel.LoadDiseasesViewModel(_diseaseStore, _modalNavigationViewModel));
    }
    
    private NavigationServiceBase CreateMedicinesNavigationService(){
        return new NavigationService(_navigationStore, () => MedicinesViewModel.LoadMedicinesViewModel(_medicineStore, _modalNavigationViewModel));
    }

    private NavigationServiceBase CreatePrescriptionsNavigationService(){
        return new NavigationService(_navigationStore, () => PrescriptionsViewModel.LoadPrescriptionsViewModel(_prescriptionStore));
    }

    private NavigationServiceBase CreateReferralsNavigationService(){
        return new NavigationService(_navigationStore, () => ReferralsViewModel.LoadReferralsViewModel(_referralStore));
    }

    private NavigationServiceBase CreateInvoicesNavigationService(){
        return new NavigationService(_navigationStore, () => InvoicesViewModel.LoadInvoicesViewModel(_invoiceStore));
    }

    private NavigationServiceBase CreatePricingNavigationService(){
        return new NavigationService(_navigationStore, () => PricingViewModel.LoadPricingViewModel(_pricingStore, _modalNavigationViewModel));
    }

    private NavigationServiceBase CreatePatientsNotesNavigationService(){
        return new NavigationService(_navigationStore, () => PatientsNotesViewModel.LoadPatientNoteViewModel(_patientNoteStore, _patientDataModalNavigationViewModel));
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
    
    private NavigationServiceBase CreatePatientDetailsNavigationService(){
        return new PatientDataModalNavigationService(_patientDataModalNavigationStore, () => PatientDetailsViewModel.LoadPatientDetailsViewModel(_patientStore, _appointmentStore));
    }
    
    private NavigationServiceBase CreateIssuePrescriptionsNavigationService(){
        return new PatientDataModalNavigationService(_patientDataModalNavigationStore, () => IssuePrescriptionViewModel.LoadIssuePrescriptionViewModel(_medicineStore, _doctorStore));
    }

    private NavigationServiceBase CreateAddNewPatientNoteNavigationService(){
        return new PatientDataModalNavigationService(_patientDataModalNavigationStore,
            () => AddNewPatientNoteViewModel.LoadAddNewPatientNoteViewModel(_doctorStore, _patientStore,
                _patientNoteStore));
    }

    private NavigationServiceBase CreateIssueReferralNavigationService(){
        return new PatientDataModalNavigationService(_patientDataModalNavigationStore, () => IssueReferralViewModel.LoadIssueReferralViewModel(_doctorStore, _diseaseStore, _referralTestStore));
    }
}