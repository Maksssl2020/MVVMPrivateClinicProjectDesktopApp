using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.AppointmentRepository;

public interface IAppointmentRepository {
    Task<AppointmentDto> SaveAppointmentAsync(SaveAppointmentRequest saveAppointmentRequest);
    Task<AppointmentDto?> GetAppointmentByIdAsync(int appointmentId);
    Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync();
    Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientIdOrDoctorIdAsync(int personId, PersonType personType);
    Task<IEnumerable<AppointmentDto>> GetUpcomingAppointmentsAsync(int amount);
    Task UpdateAppointmentStatusAsync(int appointmentId, AppointmentStatus status);
    Task<int> CountAppointmentsAsync();
    Task<int> CountAppointmentsByDoctorIdAsync(int doctorId);
}