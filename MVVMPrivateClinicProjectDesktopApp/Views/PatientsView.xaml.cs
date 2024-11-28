using System.Windows;
using System.Windows.Controls;

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
}