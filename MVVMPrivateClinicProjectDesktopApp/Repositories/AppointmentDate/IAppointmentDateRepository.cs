using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.AppointmentDate;

public interface IAppointmentDateRepository {
    Task<AppointmentDateDto> SaveAppointmentDateAsync(SaveAppointmentDateRequest appointmentDateRequest);
    Task<List<DateTime>> GetChosenPersonAppointmentsDates(int personId, PersonType personType);
}