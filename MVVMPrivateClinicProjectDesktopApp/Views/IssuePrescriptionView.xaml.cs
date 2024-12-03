using System.Windows;
using System.Windows.Controls;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class IssuePrescriptionView : UserControl {
    public IssuePrescriptionView(){
        InitializeComponent();
    }


    private void ToggleIsOpen_Click(object sender, RoutedEventArgs e){
        var viewModel = DataContext as IssuePrescriptionViewModel;
        viewModel?.ToggleIsOpen();
        Console.WriteLine("CLICK!!!");
        Console.WriteLine("IsOpen: {0}", viewModel?.IsOpen);
    }
}