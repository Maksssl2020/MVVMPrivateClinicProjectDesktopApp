using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;
using static System.Enum;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Invoice;

public class InvoiceRepository(DbContextFactory dbContextFactory, IMapper mapper, IPatientRepository patientRepository) : IInvoiceRepository {
    public async Task<InvoiceDto> SaveInvoiceAsync(SaveInvoiceRequest invoiceRequest){
        await using var context = dbContextFactory.CreateDbContext();
        
        var dateIssued = DateTime.Now;
        var dueDate = dateIssued.AddDays(7);

        var invoice = new Models.Entities.Invoice {
            Amount = invoiceRequest.Amount,
            DateIssued = dateIssued,
            DueDate = dueDate,
            IdPatient = invoiceRequest.IdPatient,
            IdPricing = invoiceRequest.IdPricing,
        };
        
        await context.AddAsync(invoice);
        await context.SaveChangesAsync();
        
        var invoiceDto = mapper.Map<InvoiceDto>(invoice);
        await AppendPatientCodeAndInvoiceStatusToInvoiceDto(invoiceDto, invoice.Status);

        return invoiceDto;
    }

    public async Task<IEnumerable<InvoiceDto>> GetAllInvoicesDtoAsync(){
        await using var context = dbContextFactory.CreateDbContext();

        var foundInvoices = await context.Invoices
            .ToListAsync();

        List<InvoiceDto> invoicesDto = [];
        
        foreach (var invoice in foundInvoices) {
            var invoiceDto = mapper.Map<InvoiceDto>(invoice);

            await AppendPatientCodeAndInvoiceStatusToInvoiceDto(invoiceDto, invoice.Status);
            
            invoicesDto.Add(invoiceDto);
        }
        
        return invoicesDto;
    }

    public async Task<decimal> CountTotalInvoicesSumAsync(){
        await using var context = dbContextFactory.CreateDbContext();

        return await context.Invoices
            .Select(invoice => invoice.Amount)
            .SumAsync();
    }

    private async Task AppendPatientCodeAndInvoiceStatusToInvoiceDto(InvoiceDto invoiceDto, string status){
        var foundPatient = await patientRepository.GetPatientByIdAsync(invoiceDto.IdPatient);

        if (foundPatient?.PatientCode != null) invoiceDto.PatientCode = foundPatient.PatientCode;
        var result = TryParse(status, out InvoiceStatus invoiceStatus);
        if (result) invoiceDto.Status = invoiceStatus;
    }
}