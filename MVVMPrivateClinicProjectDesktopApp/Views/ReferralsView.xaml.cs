using System.Windows;
using System.Windows.Controls;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class ReferralsView {
    public ReferralsView(){
        InitializeComponent();
    }
    
    private void ShowPatientNoteDetails_OnClick(object sender, RoutedEventArgs e){
        if (DataContext is not ReferralsViewModel viewModel) return;
        if (sender is not Button { DataContext: ReferralDto referralDto }) return;
        
        viewModel.SetEntityIdToShowDetails(referralDto.Id);
        viewModel.ShowReferralDetailsCommand.Execute(null);
    }
    
    private void ShowSelectPatient_OnClick(object sender, RoutedEventArgs e){
        if (DataContext is not ReferralsViewModel viewModel) return;
        
        viewModel.SetDataInAddSpecificDataToPatientStore();
        viewModel.ShowSelectPatientToAddSpecificDataModal.Execute(null);
    }

    private void ShowDeleteReferral_OnClick(object sender, RoutedEventArgs e){
        if (DataContext is not ReferralsViewModel viewModel) return;
        if (sender is not Button { DataContext: ReferralDto referralDto }) return;

        viewModel.SetEntityIdToDelete(referralDto.Id);
        
    }

    private void GeneratePdf_OnClick(object sender, RoutedEventArgs e){
        if (DataContext is not ReferralsViewModel viewModel) return;
        if (sender is not Button { DataContext: ReferralDto referralDto }) return;
        
        viewModel.GenerateReferralPdfCommand.Execute(referralDto.Id);
    }
}