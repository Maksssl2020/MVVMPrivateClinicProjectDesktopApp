using System.Windows;
using System.Windows.Controls;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class ReferralsView : UserControl {
    public ReferralsView(){
        InitializeComponent();
    }
    
    private void ShowPatientNoteDetails_OnClick(object sender, RoutedEventArgs e){
        if (DataContext is not ReferralsViewModel viewModel) return;
        if (sender is not Button { DataContext: ReferralDto referralDto }) return;
        
        viewModel.SetReferralIdToShowDetails(referralDto.Id);
        viewModel.ShowReferralDetailsCommand.Execute(null);
    }
}