using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Appointment;

public interface IAppointmentRepository {
    Task<AppointmentDto> SaveAppointmentAsync(SaveAppointmentRequest saveAppointmentRequest);
    Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync();
    Task UpdateAppointmentStatusAsync(int appointmentId, AppointmentStatus status);
    Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientIdAsync(int patientId);
    Task<IEnumerable<AppointmentDto>> GetUpcomingAppointmentsAsync(int amount);
    Task<int> CountAppointmentsAsync();
    Task<int> CountAppointmentsByDoctorIdAsync(int doctorId);
}