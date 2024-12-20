using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class InvoiceDetailsViewModel : ViewModelBase {
    private InvoiceDetailsDto _invoice = null!;
    public InvoiceDetailsDto Invoice {
        get => _invoice;
        set {
            _invoice = value;
            OnPropertyChanged();
        }
    }

    private ICommand LoadInvoiceCommand { get; set; }

    private InvoiceDetailsViewModel(InvoiceStore invoiceStore){
        LoadInvoiceCommand = new LoadInvoiceCommand(this, invoiceStore);
    }

    public static InvoiceDetailsViewModel LoadInvoiceDetailsViewModel(InvoiceStore invoiceStore){
        var invoiceDetailsViewModel = new InvoiceDetailsViewModel(invoiceStore);
        
        invoiceDetailsViewModel.LoadInvoiceCommand.Execute(null);
        
        return invoiceDetailsViewModel;
    }
}