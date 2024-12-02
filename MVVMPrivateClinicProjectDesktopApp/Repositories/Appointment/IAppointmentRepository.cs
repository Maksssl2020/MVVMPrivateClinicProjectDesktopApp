using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Appointment;

public interface IAppointmentRepository {
    Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync();
    Task UpdateAppointmentStatusAsync(int appointmentId, AppointmentStatus status);
    Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientIdAsync(int patientId);
}