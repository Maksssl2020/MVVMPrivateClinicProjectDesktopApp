using System.Windows;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class AddNewAppointmentView : WindowViewBase {
    public AddNewAppointmentView(){
        InitializeComponent();
    }
    
    private void buttonClearForm_Click(object sender, RoutedEventArgs e){
        DoctorSelector.SelectedItem = null!;
        PatientSelector.SelectedItem = null!;
        PricingSelector.SelectedItem = null!;
        DateSelector.SelectedItem = null;
        TimeSelector.SelectedItem = null;

        if (DataContext is not AddNewAppointmentViewModel viewModel) return;
        viewModel.SelectedTime = null;
        viewModel.SelectedDay = null;
    }
}