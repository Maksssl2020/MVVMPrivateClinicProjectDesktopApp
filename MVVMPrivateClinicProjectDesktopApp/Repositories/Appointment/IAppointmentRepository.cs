using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Appointment;

public interface IAppointmentRepository {
    Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync();
}