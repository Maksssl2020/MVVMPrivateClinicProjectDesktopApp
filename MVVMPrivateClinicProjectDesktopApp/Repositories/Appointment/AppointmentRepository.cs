using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Doctor;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Invoice;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Pricing;
using static System.Enum;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Appointment;

public class AppointmentRepository(
    DbContextFactory dbContextFactory,
    IMapper mapper,
    IPatientRepository patientRepository,
    IDoctorRepository doctorRepository,
    IPricingRepository pricingRepository,
    IInvoiceRepository invoiceRepository
    ) : IAppointmentRepository {
    public async Task<AppointmentDto> SaveAppointmentAsync(SaveAppointmentRequest saveAppointmentRequest){
        await using var context = dbContextFactory.CreateDbContext();

        var appointmentDate =
            new DateTime(saveAppointmentRequest.AppointmentDate, saveAppointmentRequest.AppointmentTime);

        var appointment = new Models.Entities.Appointment {
            AppointmentCost = saveAppointmentRequest.AppointmentCost,
            AppointmentDate = appointmentDate,
            IdDoctor = saveAppointmentRequest.IdDoctor,
            IdPatient = saveAppointmentRequest.IdPatient,
            IdPricing = saveAppointmentRequest.IdPricing,
        };
        
        await context.Appointments.AddAsync(appointment);
        await context.SaveChangesAsync();

        var appointmentDto = mapper.Map<AppointmentDto>(appointment);
        await CreateAppointmentDto(appointmentDto, appointment.AppointmentStatus);
        
        return appointmentDto;
    }

    public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync(){
        await using var context = dbContextFactory.CreateDbContext();
        var random = new Random();
        var foundAppointments = await context.Appointments
            .ToListAsync();
        
        foreach (var appointment in foundAppointments.Where(appointment => appointment.AppointmentDate < DateTime.Now && appointment.AppointmentStatus == AppointmentStatus.Scheduled.ToString())) {
            await UpdateAppointmentStatusAsync(appointment.Id, AppointmentStatus.Canceled);
        }

        foreach (var appointment in foundAppointments.Where(appointment => appointment.AppointmentDate < DateTime.Now && AppointmentStatus.Accepted.ToString() == appointment.AppointmentStatus)) {
            var chance = random.Next(100);
            if (chance < 8) {
                await UpdateAppointmentStatusAsync(appointment.Id, AppointmentStatus.PatientNoShow);
            }
            else {
                await UpdateAppointmentStatusAsync(appointment.Id, AppointmentStatus.Completed);
            }
        }
        
        var appointmentDtos = await CreateAppointmentsDto(foundAppointments);
        return appointmentDtos;
    }

    public async Task UpdateAppointmentStatusAsync(int appointmentId, AppointmentStatus status){
        await using var context = dbContextFactory.CreateDbContext();
        var foundAppointment = context.Appointments.FirstOrDefault(a => a.Id == appointmentId);
        if (foundAppointment == null) throw new NullReferenceException("Appointment not found!");
        
        foundAppointment.AppointmentStatus = status.ToString();
        if (!status.Equals(AppointmentStatus.Accepted) && !status.Equals(AppointmentStatus.Completed) && foundAppointment.IdInvoice != null) {
            await invoiceRepository.CancelInvoice((int) foundAppointment.IdInvoice);
        }
        
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientIdOrDoctorIdAsync(int personId, PersonType personType){
        await using var context = dbContextFactory.CreateDbContext();
        var foundAppointments = await context.Appointments
            .Where(appointment => personType.Equals(PersonType.Patient) ? appointment.IdPatient == personId : appointment.IdDoctor == personId)
            .ToListAsync();
        
        var appointmentsDto = await CreateAppointmentsDto(foundAppointments);
        return appointmentsDto;
    }

    public async Task<IEnumerable<AppointmentDto>> GetUpcomingAppointmentsAsync(int amount){
        await using var context = dbContextFactory.CreateDbContext();

       var foundAppointments = await context.Appointments
           .Where(appointment => appointment.AppointmentStatus.Equals(AppointmentStatus.Accepted.ToString()) && appointment.AppointmentDate > DateTime.Now)
           .OrderBy(appointment => appointment.AppointmentDate)
           .Take(amount)
           .ToListAsync();
       
       return await CreateAppointmentsDto(foundAppointments);
    }

    public async Task<int> CountAppointmentsAsync(){
        await using var context = dbContextFactory.CreateDbContext();
        return await context.Appointments
            .Where(appointment =>
                !appointment.AppointmentStatus.Equals(AppointmentStatus.Canceled.ToString()) &&
                !appointment.AppointmentStatus.Equals(AppointmentStatus.PatientNoShow.ToString())
                )
            .CountAsync();
    }

    public async Task<int> CountAppointmentsByDoctorIdAsync(int doctorId){
        await using var context = dbContextFactory.CreateDbContext();
        
        return await context.Appointments
            .Where(appointment => appointment.IdDoctor == doctorId)
            .CountAsync();
    }

    private async Task<List<AppointmentDto>> CreateAppointmentsDto(List<Models.Entities.Appointment> appointments){
        var appointmentsDto = new List<AppointmentDto>();
        
        foreach (var appointment in appointments) {
            var appointmentDto = mapper.Map<AppointmentDto>(appointment);
            await CreateAppointmentDto(appointmentDto, appointment.AppointmentStatus);
            
            appointmentsDto.Add(appointmentDto);
        }
        
        return appointmentsDto;
    }

    private async Task CreateAppointmentDto(AppointmentDto appointmentDto, string status){
        var foundPatient = await patientRepository.GetPatientDetailsAsync(appointmentDto.IdPatient);
        var foundDoctor = await doctorRepository.GetDoctorDetailsAsync(appointmentDto.IdDoctor);
        var foundPricing = await pricingRepository.GetPricingByIdAsync(appointmentDto.IdPricing);

        if (foundPatient != null) appointmentDto.PatientDetailsDto = foundPatient;
        if (foundDoctor != null) appointmentDto.DoctorDtoBase = foundDoctor;
        if (foundPricing != null) appointmentDto.PricingDto = foundPricing;

        appointmentDto.AppointmentStatus = status;
    }
}