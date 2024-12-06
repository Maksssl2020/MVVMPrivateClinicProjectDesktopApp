using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;
using static System.Enum;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Invoice;

public class InvoiceRepository(DbContextFactory dbContextFactory, IMapper mapper, IPatientRepository patientRepository) : IInvoiceRepository {
    public async Task<IEnumerable<InvoiceDto>> GetAllInvoicesDtoAsync(){
        await using var context = dbContextFactory.CreateDbContext();

        var foundInvoices = await context.Invoices
            .ToListAsync();

        List<InvoiceDto> invoicesDto = [];
        
        foreach (var invoice in foundInvoices) {
            var invoiceDto = mapper.Map<InvoiceDto>(invoice);

            var foundPatient = await patientRepository.GetPatientByIdAsync(invoice.IdPatient);

            if (foundPatient?.PatientCode != null) invoiceDto.PatientCode = foundPatient.PatientCode;
            var result = TryParse(invoice.Status, out InvoiceStatus invoiceStatus);
            if (result) invoiceDto.Status = invoiceStatus;
            
            invoicesDto.Add(invoiceDto);
        }
        
        return invoicesDto;
    }
}