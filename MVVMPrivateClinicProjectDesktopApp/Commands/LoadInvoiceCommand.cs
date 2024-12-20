using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadInvoiceCommand(InvoiceDetailsViewModel viewModel, InvoiceStore invoiceStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await invoiceStore.LoadInvoiceDetails();
            viewModel.Invoice = invoiceStore.SelectedInvoice;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}