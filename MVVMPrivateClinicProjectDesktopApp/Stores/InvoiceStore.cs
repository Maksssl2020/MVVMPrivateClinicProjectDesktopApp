using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class InvoiceStore {
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly List<InvoiceDto> _invoicesDto;
    private readonly Lazy<Task> _initializeLazy;

    public IEnumerable<InvoiceDto> InvoicesDto => _invoicesDto;

    public InvoiceStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;

        _invoicesDto = [];
        _initializeLazy = new Lazy<Task>(InitializeInvoices);
    }

    public async Task LoadInvoices(){
        await _initializeLazy.Value;
    }

    private async Task InitializeInvoices(){
        var loadedInvoices = await _unitOfWork.InvoiceRepository.GetAllInvoicesDtoAsync();
        
        _invoicesDto.Clear();
        _invoicesDto.AddRange(loadedInvoices);
    }
}