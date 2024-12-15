using System.Windows;
using System.Windows.Controls;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class PatientsView : UserControl {
    public PatientsView() {
        InitializeComponent();
    }
    
    private void SeePatientDetailsButton_OnClick(object sender, RoutedEventArgs e){
        var viewModel = DataContext as PatientsViewModel;
        if (sender is not Button { DataContext: PatientDto patient }) return;
        if (viewModel is null) return;
        
        viewModel.SetPatientIdToShowDetails(patient.Id);
        viewModel.ShowPatientDataModal.Execute(null);
    }

    private void ShowDeleteWarningModal_OnClick(object sender, RoutedEventArgs e){
        var viewModel = DataContext as PatientsViewModel;

        if (sender is not Button { DataContext: PatientDto patient }) return;
        
        viewModel?.SetPatientIdToDelete(patient.Id);
        viewModel?.ShowDeletePatientModal.Execute(null);
    }
}