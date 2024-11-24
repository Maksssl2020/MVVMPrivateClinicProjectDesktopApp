using System.Windows;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class PatientDetailsView : Window {
    public PatientDetailsView(int patientId) {
        InitializeComponent();
        
        var patientDetailsViewModel = new PatientDetailsViewModel(patientId);
        DataContext = patientDetailsViewModel;
    }
    
    private void buttonClose_Click(object sender, RoutedEventArgs e) => Close();
    private void MouseLeftButtonDown_Hold(object sender, MouseButtonEventArgs e) => DragMove();
}