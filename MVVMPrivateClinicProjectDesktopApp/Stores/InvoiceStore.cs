using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class InvoiceStore {
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly List<InvoiceDto> _invoicesDto;
    private readonly Lazy<Task> _initializeLazy;

    public IEnumerable<InvoiceDto> InvoicesDto => _invoicesDto;

    public event Action<InvoiceDto>? InvoiceCreated; 
    
    public InvoiceStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;

        _invoicesDto = [];
        _initializeLazy = new Lazy<Task>(InitializeInvoices);
    }

    public async Task LoadInvoices(){
        await _initializeLazy.Value;
    }

    public async Task CreateInvoice(SaveInvoiceRequest invoiceRequest){
        var savedInvoice = await _unitOfWork.InvoiceRepository.SaveInvoiceAsync(invoiceRequest);
        _invoicesDto.Add(savedInvoice);
        
        OnInvoiceCreated(savedInvoice);
    }

    private void OnInvoiceCreated(InvoiceDto invoiceDto){
        InvoiceCreated?.Invoke(invoiceDto);
    }
    
    private async Task InitializeInvoices(){
        var loadedInvoices = await _unitOfWork.InvoiceRepository.GetAllInvoicesDtoAsync();
        
        _invoicesDto.Clear();
        _invoicesDto.AddRange(loadedInvoices);
    }
}