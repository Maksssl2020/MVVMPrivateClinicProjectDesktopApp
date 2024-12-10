using System.Windows;
using System.Windows.Controls;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class PrescriptionsView : UserControl {
    public PrescriptionsView(){
        InitializeComponent();
    }

    private void ShowPrescriptionDetails_OnClick(object sender, RoutedEventArgs e){
        if (DataContext is not PrescriptionsViewModel viewModel) return;
        if (sender is not Button { DataContext: PrescriptionDto prescriptionDto }) return;
        
        viewModel.SetPrescriptionIdToSeeDetails(prescriptionDto.Id);
        viewModel.ShowPrescriptionDetailsModal.Execute(null);
    }
}