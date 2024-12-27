using System.Windows;
using System.Windows.Controls;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class ReferralTestsView : UserControl {
    public ReferralTestsView(){
        InitializeComponent();
    }
    
    private void SeeReferralTestDetailsButton_OnClick(object sender, RoutedEventArgs e){
        var viewModel = DataContext as ReferralTestsViewModel;
        if (sender is not Button { DataContext: ReferralTestDto referralTest }) return;
        if (viewModel is null) return;
        
        viewModel.SetEntityIdToShowDetails(referralTest.Id);
        viewModel.ShowReferralTestDetailsModalCommand.Execute(null);
    }

    private void DeleteReferralTest_OnClick(object sender, RoutedEventArgs e){
        var viewModel = DataContext as ReferralTestsViewModel;
        if (sender is not Button { DataContext: ReferralTestDto referralTest }) return;

        viewModel?.SetEntityIdToDelete(referralTest.Id);
    }
}