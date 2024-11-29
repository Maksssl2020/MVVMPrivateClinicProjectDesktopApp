using System.Windows;
using System.Windows.Controls;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class PatientsView : UserControl {
    public PatientsView() {
        InitializeComponent();
    }
    
    private void SeePatientDetailsButton_OnClick(object sender, RoutedEventArgs e){
        if (sender is Button { DataContext: Patient patient }) {
            new PatientDetailsView(patient.Id).ShowDialog();
        }
    }

    private void ShowDeleteWarningModal_OnClick(object sender, RoutedEventArgs e){
        var viewModel = DataContext as PatientsViewModel;

        if (sender is not Button { DataContext: Patient patient }) return;
        
        viewModel?.SetPatientIdToDelete(patient.Id);
        viewModel?.ShowDeletePatientModal.Execute(null);
    }
}