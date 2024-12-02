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
    private readonly PatientDataModalNavigationStore _patientDataModalNavigationStore;
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
        _navigationBarViewModel = new NavigationBarViewModel(
            CreateHomeNavigationService(),
            CreatePatientsNavigationService(),
            CreateDoctorsNavigationService(),
            CreateAppointmentsNavigationService(),
            CreateDiseasesNavigationService(),
            CreateMedicinesNavigationService()
        );
        _modalNavigationViewModel = new ModalNavigationViewModel(
            CreateAddNewPatientsNavigationService(),
            CreateDeletePatientsNavigationService(),
            CreatePatientDataModalNavigationService()
            );

        _patientDataModalNavigationViewModel = new PatientDataModalNavigationViewModel(
            CreatePatientDetailsNavigationService(),
            CreateIssuePrescriptionsNavigationService()
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
        return new NavigationService(_navigationStore, () => new DoctorsViewModel(_unitOfWork));
    }

    private NavigationService CreateAppointmentsNavigationService(){
        return new NavigationService(_navigationStore, () => AppointmentsViewModel.LoadAppointmentsViewModel(_appointmentStore));
    }
    
    private NavigationServiceBase CreateDiseasesNavigationService(){
        return new NavigationService(_navigationStore, () => new DiseasesViewModel(_unitOfWork));
    }
    
    private NavigationServiceBase CreateMedicinesNavigationService(){
        return new NavigationService(_navigationStore, () => new MedicinesViewModel(_unitOfWork));
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
    
    private NavigationServiceBase CreatePatientDetailsNavigationService(){
        return new PatientDataModalNavigationService(_patientDataModalNavigationStore, () => PatientDetailsViewModel.LoadPatientDetailsViewModel(_patientStore, _appointmentStore));
    }
    
    private NavigationServiceBase CreateIssuePrescriptionsNavigationService(){
        return new PatientDataModalNavigationService(_patientDataModalNavigationStore, () => new IssuePrescriptionViewModel());
    }
}