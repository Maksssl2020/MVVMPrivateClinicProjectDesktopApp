using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class AppointmentDateStore(IUnitOfWork unitOfWork) {
    public async Task<List<DateTime>> GetChosenPersonAppointmentsDates(int personId, PersonType personType){
        return await unitOfWork.AppointmentDateRepository.GetChosenPersonAppointmentsDates(personId, personType);
    }

    public async Task CreateAppointmentDate(SaveAppointmentDateRequest saveAppointmentDateRequest){
        await unitOfWork.AppointmentDateRepository.SaveAppointmentDateAsync(saveAppointmentDateRequest);
    }
}