using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class UpdateAppointmentStatusCommand(AppointmentsViewModel appointmentsViewModel, AppointmentStore appointmentStore, AppointmentStatus appointmentStatus) : RelayCommand {
    public override void Execute(object? parameter){
        var appointmentIdToUpdate = appointmentsViewModel.UpdateAppointmentStatusId;
        appointmentStore.UpdateAppointmentStatus(appointmentIdToUpdate, appointmentStatus);
        appointmentsViewModel.AppointmentsView.Refresh();
    }
}