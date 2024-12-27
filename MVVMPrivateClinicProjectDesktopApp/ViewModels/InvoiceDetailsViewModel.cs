using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class InvoiceDetailsViewModel : EntityDetailsViewModelBase<InvoiceDetailsDto> {
    private InvoiceDetailsViewModel(InvoiceStore invoiceStore)
        :base(new LoadEntityDetailsCommand<InvoiceDto, InvoiceDetailsDto>(invoiceStore)){
    }

    public static InvoiceDetailsViewModel LoadInvoiceDetailsViewModel(InvoiceStore invoiceStore){
        var invoiceDetailsViewModel = new InvoiceDetailsViewModel(invoiceStore);
        
        invoiceDetailsViewModel.LoadEntityCommand.Execute(invoiceDetailsViewModel);
        
        return invoiceDetailsViewModel;
    }
}