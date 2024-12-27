using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Base;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.InvoiceRepository;

public interface IInvoiceRepository : IBaseRepository<InvoiceDto> {
    Task<InvoiceDto> SaveInvoiceAsync(SaveInvoiceRequest invoiceRequest);
    Task<IEnumerable<InvoiceDto>> GetAllInvoicesDtoAsync();
    Task<InvoiceDetailsDto?> GetInvoiceDetailsDtoAsync(int invoiceId);
    Task<decimal> CountTotalInvoicesSumAsync();
    Task CancelInvoice(int invoiceId);
}