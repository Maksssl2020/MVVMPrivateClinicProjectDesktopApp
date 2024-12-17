using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadInvoicesDtoCommand(InvoicesViewModel viewModel, InvoiceStore invoiceStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await invoiceStore.LoadInvoices(); 
            viewModel.UpdateEntities(invoiceStore.InvoicesDto);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}