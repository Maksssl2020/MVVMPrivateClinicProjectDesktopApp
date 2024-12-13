using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Doctor;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Pricing;
using static System.Enum;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Appointment;

public class AppointmentRepository(
    DbContextFactory dbContextFactory,
    IMapper mapper,
    IPatientRepository patientRepository,
    IDoctorRepository doctorRepository,
    IPricingRepository pricingRepository
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
        var foundAppointments = await context.Appointments
            .ToListAsync();

        foreach (var appointment in foundAppointments.Where(appointment => appointment.AppointmentDate < DateTime.Now && appointment.AppointmentStatus == "Scheduled")) {
            appointment.AppointmentStatus = "Canceled";
        }
        
        var appointmentDtos = await CreateAppointmentsDto(foundAppointments);
        return appointmentDtos;
    }

    public async Task UpdateAppointmentStatusAsync(int appointmentId, AppointmentStatus status){
        await using var context = dbContextFactory.CreateDbContext();
        var foundAppointment = context.Appointments.FirstOrDefault(a => a.Id == appointmentId);
        if (foundAppointment == null) throw new NullReferenceException("Appointment not found!");
        
        foundAppointment.AppointmentStatus = status.ToString();
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientIdAsync(int patientId){
        await using var context = dbContextFactory.CreateDbContext();
        var foundPatientAppointments = await context.Appointments
            .Where(appointment => appointment.IdPatient == patientId)
            .ToListAsync();
        
        var appointmentDtos = await CreateAppointmentsDto(foundPatientAppointments);
        return appointmentDtos;
    }

    public async Task<IEnumerable<AppointmentDto>> GetUpcomingAppointmentsAsync(int amount){
        await using var context = dbContextFactory.CreateDbContext();

       var foundAppointments = await context.Appointments
           .Where(appointment => appointment.AppointmentStatus.Equals("Accepted") && appointment.AppointmentDate > DateTime.Now)
           .OrderBy(appointment => appointment.AppointmentDate)
           .Take(amount)
           .ToListAsync();
       
       return await CreateAppointmentsDto(foundAppointments);
    }

    public async Task<int> CountAppointmentsAsync(){
        await using var context = dbContextFactory.CreateDbContext();
        return await context.Appointments
            .Where(appointment =>
                !appointment.AppointmentStatus.Equals("Canceled") &&
                !appointment.AppointmentStatus.Equals("PatientNoShow")
                )
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