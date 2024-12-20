using System.Windows;
using System.Windows.Input;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class DeletePatientView : Window {
    public DeletePatientView(){
        InitializeComponent();
        ResizeMode = ResizeMode.NoResize;
    }
    
    private void buttonClose_Click(object sender, RoutedEventArgs e) => Close();
    private void MouseLeftButtonDown_Hold(object sender, MouseButtonEventArgs e) => DragMove();
}