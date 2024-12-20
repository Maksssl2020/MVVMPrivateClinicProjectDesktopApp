using System.Windows;
using System.Windows.Controls;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Views;

public partial class InvoicesView : UserControl {
    public InvoicesView(){
        InitializeComponent();
    }
    
    private void ShowInvoiceDetails_OnClick(object sender, RoutedEventArgs e){
        if (DataContext is not InvoicesViewModel viewModel) return;
        if (sender is not Button { DataContext: InvoiceDto invoiceDto }) return;

        viewModel.SetInvoiceIdToShowDetails(invoiceDto.Id);
        viewModel.ShowInvoiceDetailsModalCommand.Execute(null);
    }
}