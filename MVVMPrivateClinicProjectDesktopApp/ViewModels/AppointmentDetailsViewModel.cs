using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class AppointmentDetailsViewModel(AppointmentStore appointmentStore)
    : EntityDetailsViewModelBase<AppointmentDto>(new LoadEntityDetailsCommand<AppointmentDto, AppointmentDto>(appointmentStore)) {
    
    public static AppointmentDetailsViewModel LoadAppointmentDetailsViewModel(AppointmentStore appointmentStore){
        var appointmentDetailsViewModel = new AppointmentDetailsViewModel(appointmentStore);
        
        appointmentDetailsViewModel.LoadEntityCommand.Execute(appointmentDetailsViewModel);
        
        return appointmentDetailsViewModel;
    }
}