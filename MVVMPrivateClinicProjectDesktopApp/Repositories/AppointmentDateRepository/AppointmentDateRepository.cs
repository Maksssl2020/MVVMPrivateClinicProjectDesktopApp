using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.AppointmentDateRepository;

public class AppointmentDateRepository(DbContextFactory dbContextFactory, IMapper mapper) : IAppointmentDateRepository {
    public async Task<AppointmentDateDto> SaveAppointmentDateAsync(SaveAppointmentDateRequest appointmentDateRequest){
        await using var context = dbContextFactory.CreateDbContext();
        
        var appointmentDate =
            new DateTime(appointmentDateRequest.AppointmentDate, appointmentDateRequest.AppointmentTime);

        var appointmentsCalendar = new Models.Entities.AppointmentDate {
            AppointmentDate1 = appointmentDate,
            IdDoctor = appointmentDateRequest.IdDoctor,
            IdPatient = appointmentDateRequest.IdPatient,
        };
        
        await context.AppointmentDates.AddAsync(appointmentsCalendar);
        await context.SaveChangesAsync();


        var appointmentDateDto = mapper.Map<AppointmentDateDto>(appointmentsCalendar);
        appointmentDateDto.AppointmentDate = DateOnly.FromDateTime(appointmentsCalendar.AppointmentDate1);
        appointmentDateDto.AppointmentTime = TimeOnly.FromDateTime(appointmentsCalendar.AppointmentDate1);
        
        return appointmentDateDto;
    }

    public async Task<List<DateTime>> GetChosenPersonAppointmentsDates(int personId, PersonType personType){
        await using var context = dbContextFactory.CreateDbContext();

        return await context.AppointmentDates
            .Where(appointment => (personType == PersonType.Patient ? appointment.IdPatient : appointment.IdDoctor) == personId)
            .Select(appointment => appointment.AppointmentDate1)
            .ToListAsync();
    }

}