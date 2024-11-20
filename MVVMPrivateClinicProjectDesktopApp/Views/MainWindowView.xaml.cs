using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindowView : Window {
    public MainWindowView(){
        InitializeComponent();
    }

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, uint wMsg, IntPtr wParam, IntPtr lParam);
    
    private void controlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e){
        var helper = new WindowInteropHelper(this);
        SendMessage(helper.Handle, 161, 2, 0);
    }

    private void buttonClose_Click(object sender, RoutedEventArgs e){
        Application.Current.Shutdown();
    }

    private void buttonMinimize_Click(object sender, RoutedEventArgs e){
        WindowState = WindowState.Minimized;
    }
}