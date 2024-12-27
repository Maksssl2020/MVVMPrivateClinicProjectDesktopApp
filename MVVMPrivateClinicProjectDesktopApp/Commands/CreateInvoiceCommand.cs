using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class CreateInvoiceCommand(AddNewInvoiceViewModel viewModel, InvoiceStore invoiceStore, Action resetForm): AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            var saveInvoiceRequest = new SaveInvoiceRequest {
                Amount = viewModel.SelectedPricing.Price,
                IdPatient = viewModel.SelectedPatient.Id,
                IdPricing = viewModel.SelectedPricing.Id
            };

            await invoiceStore.CreateEntity(saveInvoiceRequest);
            resetForm.Invoke();
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}