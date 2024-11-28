using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Services;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class NavigationBarViewModel(
    NavigationServiceBase homeNavigationService,
    NavigationServiceBase patientsNavigationService,
    NavigationServiceBase doctorsNavigationService,
    NavigationServiceBase diseasesNavigationService,
    NavigationServiceBase medicinesNavigationService)
    : ViewModelBase {
    
    public ICommand ShowHomeViewCommand { get; set; } = new NavigateCommand(homeNavigationService);
    public ICommand ShowPatientsViewCommand { get; set; } = new NavigateCommand(patientsNavigationService);
    public ICommand ShowDoctorsViewCommand { get; set; } = new NavigateCommand(doctorsNavigationService);
    public ICommand ShowDiseasesViewCommand { get; set; } = new NavigateCommand(diseasesNavigationService);
    public ICommand ShowMedicinesViewCommand { get; set; } = new NavigateCommand(medicinesNavigationService);
}