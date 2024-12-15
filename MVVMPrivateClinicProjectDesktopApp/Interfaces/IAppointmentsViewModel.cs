using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Interfaces;

public interface IAppointmentsViewModel {
    void UpdateAppointments(IEnumerable<AppointmentDto> appointments);
}