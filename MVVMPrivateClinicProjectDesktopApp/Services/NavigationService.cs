using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Services;

public class NavigationService(NavigationStore navigationStore, Func<ViewModelBase> createViewModel) : NavigationServiceBase {

    public override void Navigate(){
        var viewModel = createViewModel();
        var viewTitle = GetViewTitleDependsOnCreatedViewModel(viewModel);
        navigationStore.ChangeCurrentViewModel(viewModel, viewTitle);
    }

    private static string GetViewTitleDependsOnCreatedViewModel(ViewModelBase viewModel) => viewModel switch {
        HomeViewModel => "Home",
        PatientsViewModel => "Patients",
        DoctorsViewModel => "Doctors",
        AppointmentsViewModel => "Appointments",
        DiseasesViewModel => "Diseases",
        MedicinesViewModel => "Medicines",
        _ => "Home",
    };
}