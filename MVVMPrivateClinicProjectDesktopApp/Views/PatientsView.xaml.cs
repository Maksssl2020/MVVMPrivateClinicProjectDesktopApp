using System.Windows;
using System.Windows.Controls;
using MVVMPrivateClinicProjectDesktopApp.Models.Entities;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class PatientsView : UserControl {
    public PatientsView() {
        InitializeComponent();
    }
    
    private void AddPatientButton_OnClick(object sender, RoutedEventArgs e){
        new AddNewPatientView().ShowDialog();
    }

    private void SeePatientDetailsButton_OnClick(object sender, RoutedEventArgs e){
        if (sender is Button { DataContext: Patient patient }) {
            new PatientDetailsView(patient.Id).ShowDialog();
        }
    }
}