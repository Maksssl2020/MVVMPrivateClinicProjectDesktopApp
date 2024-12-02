using System.Windows;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class PatientDataModalView : Window {
    public PatientDataModalView() {
        InitializeComponent();
        ResizeMode = ResizeMode.NoResize;
    }
    
    private void buttonClose_Click(object sender, RoutedEventArgs e) => Close();
    private void MouseLeftButtonDown_Hold(object sender, MouseButtonEventArgs e) => DragMove();
}