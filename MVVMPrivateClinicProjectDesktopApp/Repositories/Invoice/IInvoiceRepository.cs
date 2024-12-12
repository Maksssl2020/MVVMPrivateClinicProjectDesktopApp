using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Invoice;

public interface IInvoiceRepository {
    Task<InvoiceDto> SaveInvoiceAsync(SaveNewInvoiceRequest invoiceRequest);
    Task<IEnumerable<InvoiceDto>> GetAllInvoicesDtoAsync();
}