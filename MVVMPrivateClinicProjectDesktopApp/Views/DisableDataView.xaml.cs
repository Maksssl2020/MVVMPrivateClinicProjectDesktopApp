using System.Windows;
using System.Windows.Controls;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class DisableDataView : UserControl {
    public DisableDataView(){
        InitializeComponent();
    }
    
    private void RestoreData_OnClick(object sender, RoutedEventArgs e){
        if (DataContext is not DisabledDataViewModel viewModel) return;
        if(sender is not Button {DataContext: DisabledDataDto disabledDataDto}) return;
        
        viewModel.RestoreDisableData(disabledDataDto.Id);
    }
}