using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Invoice;

public interface IInvoiceRepository {
    Task<IEnumerable<InvoiceDto>> GetAllInvoicesDtoAsync();
}