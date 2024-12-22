using System.Windows;
using System.Windows.Controls;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class DoctorsView : UserControl {
    public DoctorsView(){
        InitializeComponent();
    }
    
    private void SeeDoctorDetailsButton_OnClick(object sender, RoutedEventArgs e){
        var viewModel = DataContext as DoctorsViewModel;
        if (sender is not Button { DataContext: DoctorDto patient }) return;
        if (viewModel is null) return;
        
        viewModel.SetDoctorIdToShowDetails(patient.Id);
        viewModel.ShowDoctorDetailsCommand.Execute(null);
    }
}