using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Services;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class NavigationBarViewModel(
    NavigationServiceBase homeNavigationService,
    NavigationServiceBase patientsNavigationService,
    NavigationServiceBase doctorsNavigationService,
    NavigationServiceBase appointmentsNavigationService,
    NavigationServiceBase diseasesNavigationService,
    NavigationServiceBase medicinesNavigationService,
    NavigationServiceBase prescriptionsNavigationService,
    NavigationServiceBase referralsNavigationService,
    NavigationServiceBase invoicesNavigationService,
    NavigationServiceBase pricingNavigationService,
    NavigationServiceBase patientsNotesNavigationService,
    NavigationServiceBase referralTestsNavigationService,
    NavigationServiceBase disabledDataNavigationService
    )
    : ViewModelBase {
    
    public ICommand ShowHomeViewCommand { get; set; } = new NavigateCommand(homeNavigationService);
    public ICommand ShowPatientsViewCommand { get; set; } = new NavigateCommand(patientsNavigationService);
    public ICommand ShowDoctorsViewCommand { get; set; } = new NavigateCommand(doctorsNavigationService);
    public ICommand ShowAppointmentsViewCommand { get; set; } = new NavigateCommand(appointmentsNavigationService);
    public ICommand ShowDiseasesViewCommand { get; set; } = new NavigateCommand(diseasesNavigationService);
    public ICommand ShowMedicinesViewCommand { get; set; } = new NavigateCommand(medicinesNavigationService);
    public ICommand ShowPrescriptionsViewCommand { get; set; } = new NavigateCommand(prescriptionsNavigationService);
    public ICommand ShowReferralsViewCommand { get; set; } = new NavigateCommand(referralsNavigationService);
    public ICommand ShowInvoicesViewCommand { get; set; } = new NavigateCommand(invoicesNavigationService);
    public ICommand ShowPricingViewCommand { get; set; } = new NavigateCommand(pricingNavigationService);
    public ICommand ShowPatientsNotesViewCommand { get; set; } = new NavigateCommand(patientsNotesNavigationService);
    public ICommand ShowReferralTestsViewCommand { get; set; } = new NavigateCommand(referralTestsNavigationService);
    public ICommand ShowDisabledDataViewCommand { get; set; } = new NavigateCommand(disabledDataNavigationService);
}