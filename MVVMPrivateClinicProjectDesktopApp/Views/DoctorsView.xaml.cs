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
        if (sender is not Button { DataContext: DoctorDto doctor }) return;
        if (viewModel is null) return;
        
        viewModel.SetEntityIdToShowDetails(doctor.Id);
        viewModel.ShowDoctorDetailsCommand.Execute(null);
    }
    
    private void ShowDeleteDoctorModal_OnClick(object sender, RoutedEventArgs e){
        var viewModel = DataContext as DoctorsViewModel;
        if (sender is not Button { DataContext: DoctorDto doctor }) return;

        viewModel?.SetEntityIdToDelete(doctor.Id);
    }
}