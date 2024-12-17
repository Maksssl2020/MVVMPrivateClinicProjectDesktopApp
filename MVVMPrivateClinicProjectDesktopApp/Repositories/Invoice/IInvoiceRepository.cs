using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Invoice;

public interface IInvoiceRepository {
    Task<InvoiceDto> SaveInvoiceAsync(SaveInvoiceRequest invoiceRequest);
    Task<IEnumerable<InvoiceDto>> GetAllInvoicesDtoAsync();
    Task<decimal> CountTotalInvoicesSumAsync();
}