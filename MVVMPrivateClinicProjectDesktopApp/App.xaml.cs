using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Navigation;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Doctor;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;
using MVVMPrivateClinicProjectDesktopApp.Services;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;
using MVVMPrivateClinicProjectDesktopApp.Views;
using NavigationService = MVVMPrivateClinicProjectDesktopApp.Services.NavigationService;

namespace MVVMPrivateClinicProjectDesktopApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application {

    private readonly ServiceProvider _serviceProvider;
    private readonly NavigationStore _navigationStore;
    private readonly ModalNavigationStore _modalNavigationStore;
    private readonly PatientStore _patientStore;
    private readonly NavigationBarViewModel _navigationBarViewModel;
    private readonly ModalNavigationViewModel _modalNavigationViewModel;
    
    public App(){
        _navigationStore = new NavigationStore();
        _patientStore = new PatientStore();
        _modalNavigationStore = new ModalNavigationStore();
        _navigationBarViewModel = new NavigationBarViewModel(
            CreateHomeNavigationService(),
            CreatePatientsNavigationService(),
            CreateDoctorsNavigationService(),
            CreateDiseasesNavigationService(),
            CreateMedicinesNavigationService()
        );
        _modalNavigationViewModel = new ModalNavigationViewModel(
            CreateAddNewPatientsNavigationService(),
            CreateDeletePatientsNavigationService()
            );
        
        IServiceCollection services = new ServiceCollection();
        services.AddAutoMapper(typeof(App));
        var mapper = services.BuildServiceProvider().GetService<IMapper>();
        MyMapper.Mapper = mapper;
        _serviceProvider = services.BuildServiceProvider();
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
        return new NavigationService(_navigationStore, () => new DoctorsViewModel());
    }
    
    private NavigationServiceBase CreateDiseasesNavigationService(){
        return new NavigationService(_navigationStore, () => new DiseasesViewModel());
    }
    
    private NavigationServiceBase CreateMedicinesNavigationService(){
        return new NavigationService(_navigationStore, () => new MedicinesViewModel());
    }

    private NavigationServiceBase CreateAddNewPatientsNavigationService(){
        return new ModalNavigationService(_modalNavigationStore, () => new AddNewPatientViewModel(_patientStore));
    }

    private NavigationServiceBase CreateDeletePatientsNavigationService(){
        return new ModalNavigationService(_modalNavigationStore, () => new DeletePatientViewModel(_patientStore));
    }
}