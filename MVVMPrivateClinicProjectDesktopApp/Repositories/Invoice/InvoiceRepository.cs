using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Pricing;
using static System.Enum;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Invoice;

public class InvoiceRepository(
    DbContextFactory dbContextFactory,
    IMapper mapper,
    IPatientRepository patientRepository,
    IPricingRepository pricingRepository
    ) : IInvoiceRepository {
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

    public async Task<InvoiceDetailsDto?> GetInvoiceDetailsDtoAsync(int invoiceId){
        await using var context = dbContextFactory.CreateDbContext();

        var foundInvoice = await context.Invoices
            .Where(i => i.Id == invoiceId)
            .FirstOrDefaultAsync();

        var invoiceDetailsDto = mapper.Map<InvoiceDetailsDto>(foundInvoice);

        if (foundInvoice == null) {
            return null;
        }

        var foundPatientDetails = await patientRepository.GetPatientDetailsAsync(foundInvoice.IdPatient);
        var foundPricing = await pricingRepository.GetPricingByIdAsync(foundInvoice.IdPricing);

        if (foundPatientDetails != null) invoiceDetailsDto.PatientDetailsDto = foundPatientDetails;
        if (foundPricing != null) invoiceDetailsDto.PricingDto = foundPricing;
        
        var result = TryParse(foundInvoice.Status, out InvoiceStatus parsedInvoiceStatus );
        if (result) {
            invoiceDetailsDto.Status = parsedInvoiceStatus;
        }
        
        return invoiceDetailsDto;
    }

    public async Task<decimal> CountTotalInvoicesSumAsync(){
        await using var context = dbContextFactory.CreateDbContext();

        return await context.Invoices
            .Select(invoice => invoice.Amount)
            .SumAsync();
    }

    public async Task CancelInvoice(int invoiceId) {
        await using var context = dbContextFactory.CreateDbContext();

        var foundInvoice = await context.Invoices
            .Where(invoice => invoice.Id == invoiceId)
            .FirstOrDefaultAsync();

        if (foundInvoice != null) {
            foundInvoice.Status = InvoiceStatus.Canceled.ToString();
            await context.SaveChangesAsync();
        }
    }

    private async Task AppendPatientCodeAndInvoiceStatusToInvoiceDto(InvoiceDto invoiceDto, string status){
        var foundPatient = await patientRepository.GetPatientByIdAsync(invoiceDto.IdPatient);

        if (foundPatient?.PatientCode != null) invoiceDto.PatientCode = foundPatient.PatientCode;
        var result = TryParse(status, out InvoiceStatus invoiceStatus);
        if (result) invoiceDto.Status = invoiceStatus;
    }
}