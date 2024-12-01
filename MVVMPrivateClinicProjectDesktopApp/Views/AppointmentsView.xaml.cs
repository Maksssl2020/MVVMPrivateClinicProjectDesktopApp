using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class AppointmentsView : UserControl {
    public AppointmentsView(){
        InitializeComponent();
    }

    private void ButtonNextPage_Click(object sender, RoutedEventArgs e){
        var viewModel = DataContext as AppointmentsViewModel;
        viewModel?.NextPage();
    }
    
    private void ButtonBackPage_Click(object sender, RoutedEventArgs e){
        var viewModel = DataContext as AppointmentsViewModel;
        viewModel?.BackPage();
    }
}