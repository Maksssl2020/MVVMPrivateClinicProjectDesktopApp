using System.Windows;
using System.Windows.Controls;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class PricingView : UserControl {
    public PricingView(){
        InitializeComponent();
    }

    private void ShowPricingDetails_OnClick(object sender, RoutedEventArgs e){
        if (DataContext is not PricingViewModel viewModel) return;
        if (sender is not Button { DataContext: PricingDto pricingDto }) return;
        
        viewModel.SetEntityIdToShowDetails(pricingDto.Id);
        viewModel.ShowPricingDetailsModalCommand.Execute(null);
    }
    
    private void ShowDeletePricingModal_OnClick(object sender, RoutedEventArgs e){
        if (DataContext is not PricingViewModel viewModel) return;
        if (sender is not Button { DataContext: PricingDto pricingDto }) return;
        
        viewModel.SetEntityIdToDelete(pricingDto.Id);
    }
}