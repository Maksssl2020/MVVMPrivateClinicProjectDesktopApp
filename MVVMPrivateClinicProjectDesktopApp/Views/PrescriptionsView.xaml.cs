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
        
        viewModel.SetEntityIdToShowDetails(prescriptionDto.Id);
        viewModel.ShowPrescriptionDetailsModal.Execute(null);
    }

    private void ShowSelectPatient_OnClick(object sender, RoutedEventArgs e){
        if (DataContext is not PrescriptionsViewModel viewModel) return;
        
        viewModel.SetDataInAddSpecificDataToPatientStore();
        viewModel.ShowSelectPatientToAddSpecificDataModal.Execute(null);
    }
    
    private void ShowDeletePrescriptionModal_OnClick(object sender, RoutedEventArgs e){
        if (DataContext is not PrescriptionsViewModel viewModel) return;
        if (sender is not Button { DataContext: PrescriptionDto prescriptionDto }) return;
        
        viewModel.SetEntityIdToDelete(prescriptionDto.Id);
    }

    private void GeneratePdf_OnClick(object sender, RoutedEventArgs e){
        if (DataContext is not PrescriptionsViewModel viewModel) return;
        if (sender is not Button { DataContext: PrescriptionDto prescriptionDto }) return;
        
        viewModel.GeneratePrescriptionPdf(prescriptionDto.Id);
    }
}