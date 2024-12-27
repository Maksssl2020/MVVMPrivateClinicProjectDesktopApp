using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Base;
using MVVMPrivateClinicProjectDesktopApp.Repositories.PatientRepository;
using MVVMPrivateClinicProjectDesktopApp.Repositories.PricingRepository;
using static System.Enum;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.InvoiceRepository;

public class InvoiceRepository(
    DbContextFactory dbContextFactory,
    IMapper mapper,
    IPatientRepository patientRepository,
    IPricingRepository pricingRepository
    ) : BaseRepository<Invoice, InvoiceDto>(dbContextFactory, mapper), IInvoiceRepository {
    private readonly DbContextFactory _dbContextFactory = dbContextFactory;
    private readonly IMapper _mapper = mapper;

    public async Task<InvoiceDto> SaveInvoiceAsync(SaveInvoiceRequest invoiceRequest){
        await using var context = _dbContextFactory.CreateDbContext();
        
        var dateIssued = DateTime.Now;
        var dueDate = dateIssued.AddDays(7);

        var invoice = new Invoice {
            Amount = invoiceRequest.Amount,
            DateIssued = dateIssued,
            DueDate = dueDate,
            IdPatient = invoiceRequest.IdPatient,
            IdPricing = invoiceRequest.IdPricing,
        };
        
        await context.AddAsync(invoice);
        await context.SaveChangesAsync();
        
        var invoiceDto = _mapper.Map<InvoiceDto>(invoice);
        await AppendPatientCodeToInvoice(invoiceDto);

        return invoiceDto;
    }

    public async Task<IEnumerable<InvoiceDto>> GetAllInvoicesDtoAsync(){
        var foundInvoices = await GetAllEntitiesAsync();

        List<InvoiceDto> invoicesDto = [];
        
        foreach (var invoice in foundInvoices) {
            var invoiceDto = _mapper.Map<InvoiceDto>(invoice);

            await AppendPatientCodeToInvoice(invoiceDto);
            
            invoicesDto.Add(invoiceDto);
        }
        
        return invoicesDto;
    }

    public async Task<InvoiceDetailsDto?> GetInvoiceDetailsDtoAsync(int invoiceId){
        await using var context = _dbContextFactory.CreateDbContext();

        var foundInvoice = await context.Invoices
            .Where(i => i.Id == invoiceId)
            .FirstOrDefaultAsync();

        var invoiceDetailsDto = _mapper.Map<InvoiceDetailsDto>(foundInvoice);

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
        await using var context = _dbContextFactory.CreateDbContext();

        return await context.Invoices
            .Select(invoice => invoice.Amount)
            .SumAsync();
    }

    public async Task CancelInvoice(int invoiceId) {
        await using var context = _dbContextFactory.CreateDbContext();

        var foundInvoice = await context.Invoices
            .Where(invoice => invoice.Id == invoiceId)
            .FirstOrDefaultAsync();

        if (foundInvoice != null) {
            foundInvoice.Status = InvoiceStatus.Canceled.ToString();
            await context.SaveChangesAsync();
        }
    }

    private async Task AppendPatientCodeToInvoice(InvoiceDto invoiceDto){
        var foundPatient = await patientRepository.GetPatientByIdAsync(invoiceDto.IdPatient);
        if (foundPatient?.PatientCode != null) invoiceDto.PatientCode = foundPatient.PatientCode;
    }
}