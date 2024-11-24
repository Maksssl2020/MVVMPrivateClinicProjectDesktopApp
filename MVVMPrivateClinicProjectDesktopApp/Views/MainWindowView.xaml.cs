using System.Windows;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;
using Application = System.Windows.Application;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindowView : Window {
    public MainWindowView(){
        InitializeComponent();
        ResizeMode = ResizeMode.NoResize;
    }

    private void MouseLeftButtonDown_Press(object sender, MouseButtonEventArgs e) => DragMove();
    private void buttonClose_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
    private void buttonMinimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
}