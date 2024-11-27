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
    private readonly PatientStore _patientStore;
    private readonly NavigationBarViewModel _navigationBarViewModel;
    
    public App(){
        _navigationStore = new NavigationStore();
        _patientStore = new PatientStore();
        _navigationBarViewModel = new NavigationBarViewModel(
            CreateHomeNavigationService(),
            CreatePatientsNavigationService(),
            CreateDoctorsNavigationService(),
            CreateDiseasesNavigationService(),
            CreateMedicinesNavigationService()
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

    private NavigationService CreateHomeNavigationService(){
        return new NavigationService(_navigationStore, () => new HomeViewModel());
    }
    
    private NavigationService CreatePatientsNavigationService(){
        return new NavigationService(_navigationStore, () => new PatientsViewModel(_patientStore));
    }
    
    private NavigationService CreateDoctorsNavigationService(){
        return new NavigationService(_navigationStore, () => new DoctorsViewModel());
    }
    
    private NavigationService CreateDiseasesNavigationService(){
        return new NavigationService(_navigationStore, () => new DiseasesViewModel());
    }
    
    private NavigationService CreateMedicinesNavigationService(){
        return new NavigationService(_navigationStore, () => new MedicinesViewModel());
    }
}