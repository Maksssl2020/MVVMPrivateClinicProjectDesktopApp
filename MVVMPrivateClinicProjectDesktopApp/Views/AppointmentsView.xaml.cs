using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
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

    private void ButtonAcceptAppointment_Click(object sender, RoutedEventArgs e){
        var viewModel = DataContext as AppointmentsViewModel;

        if (sender is not Button { DataContext: AppointmentDto appointment }) return;
        if (viewModel == null) return;
        
        viewModel.UpdateAppointmentStatusId = appointment.Id;
        viewModel.AcceptAppointmentCommand.Execute(null);
    }
    
    private void ButtonCancelAppointment_Click(object sender, RoutedEventArgs e){
        var viewModel = DataContext as AppointmentsViewModel;

        if (sender is not Button { DataContext: AppointmentDto appointment }) return;
        if (viewModel == null) return;
        
        viewModel.UpdateAppointmentStatusId = appointment.Id;
        viewModel.CancelAppointmentCommand.Execute(null);
    }
}