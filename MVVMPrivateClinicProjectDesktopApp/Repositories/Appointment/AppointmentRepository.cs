using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Doctor;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Appointment;

public class AppointmentRepository(DbContextFactory dbContextFactory, IPatientRepository patientRepository, IDoctorRepository doctorRepository) : IAppointmentRepository {
    public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync(){
        await using var context = dbContextFactory.CreateDbContext();
        var foundAppointments = await context.Appointments
            .ToListAsync();

        var appointmentDtos = await CreateAppointmentDtos(foundAppointments);
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

        Console.WriteLine(foundPatientAppointments.Count);
        Console.WriteLine("Patient ID: {0}", patientId);
        
        var appointmentDtos = await CreateAppointmentDtos(foundPatientAppointments);
        return appointmentDtos;
    }

    private async Task<List<AppointmentDto>> CreateAppointmentDtos(List<Models.Entities.Appointment> appointments){
        var appointmentDtos = new List<AppointmentDto>();
        
        foreach (var appointment in appointments) {
            var foundPatient = await patientRepository.GetPatientByIdAsync(appointment.IdPatient);
            var foundDoctor = await doctorRepository.GetDoctorByIdAsync(appointment.IdDoctor);

            if (foundDoctor == null || foundPatient == null) {
                throw new NullReferenceException("Doctor or Patient not found");
            };

            var appointmentDto = new AppointmentDto {
                Id = appointment.Id,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentStatus = appointment.AppointmentStatus,
                DoctorFirstName = foundDoctor.FirstName,
                DoctorLastName = foundDoctor.LastName,
                DoctorSpecialization = foundDoctor.DoctorSpecialization!,
                PatientFirstName = foundPatient.FirstName,
                PatientLastName = foundPatient.LastName,
                PatientCode = foundPatient.PatientCode!
            };
            
            appointmentDtos.Add(appointmentDto);
        }
        
        return appointmentDtos;
    }
}