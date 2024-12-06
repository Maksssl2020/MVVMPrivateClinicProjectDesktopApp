using System.Windows;
using System.Windows.Input;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public class WindowViewBase : Window {
    protected WindowViewBase(){
        ResizeMode = ResizeMode.NoResize;
    }

    protected void buttonClose_Click(object sender, RoutedEventArgs e) => Close();
    protected void MouseLeftButtonDown_Hold(object sender, MouseButtonEventArgs e) => DragMove();
}