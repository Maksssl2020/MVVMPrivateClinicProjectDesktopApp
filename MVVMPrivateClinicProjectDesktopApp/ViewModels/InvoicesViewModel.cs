using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class InvoicesViewModel : ViewModelBase {
    private readonly ObservableCollection<InvoiceDto> _invoices;
    public ICollectionView InvoicesView { get; set; }

    private ICommand LoadInvoicesCommand { get; set; }

    private InvoicesViewModel(InvoiceStore invoiceStore){
        _invoices = [];
        InvoicesView = CollectionViewSource.GetDefaultView(_invoices);

        LoadInvoicesCommand = new LoadInvoicesDtoCommand(this, invoiceStore);
    }

    public static InvoicesViewModel LoadInvoicesViewModel(InvoiceStore invoiceStore){
        var invoicesViewModel = new InvoicesViewModel(invoiceStore);
        
        invoicesViewModel.LoadInvoicesCommand.Execute(null);
        
        return invoicesViewModel;
    }
    
    public void UpdateInvoices(IEnumerable<InvoiceDto> invoices){
        _invoices.Clear();

        foreach (var invoice in invoices) {
            _invoices.Add(invoice);
        }
    }
}