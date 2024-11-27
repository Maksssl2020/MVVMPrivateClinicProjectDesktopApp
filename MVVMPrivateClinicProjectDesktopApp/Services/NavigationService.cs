using System.Diagnostics;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Services;

public class NavigationService(NavigationStore navigationStore, Func<ViewModelBase> createViewModel) {

    public void Navigate(){
        var viewModel = createViewModel();
        var viewTitle = GetViewTitleDependsOnCreatedViewModel(viewModel);
        navigationStore.ChangeCurrentViewModel(viewModel, viewTitle);
    }

    private static string GetViewTitleDependsOnCreatedViewModel(ViewModelBase viewModel) => viewModel switch {
        HomeViewModel => "Home",
        PatientsViewModel => "Patients",
        DoctorsViewModel => "Doctors",
        DiseasesViewModel => "Diseases",
        MedicinesViewModel => "Medicines",
        _ => "Home",
    };
}