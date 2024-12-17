using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class InvoicesViewModel : DisplayEntitiesViewModelBase<InvoiceDto> {
   private ICommand LoadInvoicesCommand { get; set; }
   public ICommand ShowAddNewInvoiceModalCommand { get; set; }
   
    private InvoicesViewModel(InvoiceStore invoiceStore, ModalNavigationViewModel modalNavigationViewModel)
        :base([SortingOptions.IdAscending, SortingOptions.IdDescending, SortingOptions.PriceAscending, SortingOptions.PriceDescending, SortingOptions.DateAscending, SortingOptions.DateDescending]) {
        LoadInvoicesCommand = new LoadInvoicesDtoCommand(this, invoiceStore);
        ShowAddNewInvoiceModalCommand = modalNavigationViewModel.ShowAddNewInvoiceModal;
        
        invoiceStore.InvoiceCreated += OnInvoiceCreated;
    }

    public static InvoicesViewModel LoadInvoicesViewModel(InvoiceStore invoiceStore, ModalNavigationViewModel modalNavigationViewModel){
        var invoicesViewModel = new InvoicesViewModel(invoiceStore, modalNavigationViewModel);
        
        invoicesViewModel.LoadInvoicesCommand.Execute(null);
        
        return invoicesViewModel;
    }

    public override void UpdateEntities(IEnumerable<InvoiceDto> entities){
        Entities.Clear();

        foreach (var entity in entities) {
            Entities.Add(entity);
        }
        
        SelectedSortingOption = SortingOptions.IdAscending;
    }

    protected override void SortEntities(){
        if (SelectedSortingOption is SortingOptions.IdAscending or SortingOptions.IdDescending) {
            ApplySortingOptions.ApplySortingWithOneProperty(EntitiesView, SelectedSortingOption, nameof(InvoiceDto.Id));
        }

        if (SelectedSortingOption is SortingOptions.DateAscending or SortingOptions.DateDescending) {
            ApplySortingOptions.ApplySortingWithOneProperty(EntitiesView, SelectedSortingOption, nameof(InvoiceDto.DateIssued));
        }

        ApplySortingOptions.ApplySortingWithOneProperty(EntitiesView, SelectedSortingOption, nameof(InvoiceDto.Amount));
    }

    protected override bool ApplyFilter(object obj){
        if (obj is not InvoiceDto invoiceDto) {
            return false;
        }

        if (string.IsNullOrWhiteSpace(Filter)) {
            return true;
        }
        
        var filter = Filter.Trim().ToLower();
        return invoiceDto.PatientCode.Contains(filter, StringComparison.CurrentCultureIgnoreCase);
    }

    private void OnInvoiceCreated(InvoiceDto invoiceDto){
        Entities.Add(invoiceDto);
    }
}