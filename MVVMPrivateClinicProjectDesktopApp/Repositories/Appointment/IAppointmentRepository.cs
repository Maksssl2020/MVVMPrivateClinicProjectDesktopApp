using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Appointment;

public interface IAppointmentRepository {
    Task<AppointmentDto> SaveAppointmentAsync(SaveAppointmentRequest saveAppointmentRequest);
    Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync();
    Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientIdOrDoctorIdAsync(int personId, PersonType personType);
    Task<IEnumerable<AppointmentDto>> GetUpcomingAppointmentsAsync(int amount);
    Task UpdateAppointmentStatusAsync(int appointmentId, AppointmentStatus status);
    Task<int> CountAppointmentsAsync();
    Task<int> CountAppointmentsByDoctorIdAsync(int doctorId);
}