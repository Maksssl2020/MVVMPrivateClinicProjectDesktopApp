using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class AppointmentDateStore(IUnitOfWork unitOfWork) {
    public async Task<List<DateTime>> GetChosenPersonAppointmentsDates(int personId, AppointmentDatePersonType appointmentDatePersonType){
        return await unitOfWork.AppointmentDateRepository.GetChosenPersonAppointmentsDates(personId, appointmentDatePersonType);
    }

    public async Task CreateAppointmentDate(SaveAppointmentDateRequest saveAppointmentDateRequest){
        await unitOfWork.AppointmentDateRepository.SaveAppointmentDateAsync(saveAppointmentDateRequest);
    }
}