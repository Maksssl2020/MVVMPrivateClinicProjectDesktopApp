using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class UpdateAppointmentStatusCommand(AppointmentsViewModel appointmentsViewModel, AppointmentStore appointmentStore, AppointmentStatus appointmentStatus) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            var appointmentIdToUpdate = appointmentsViewModel.UpdateAppointmentStatusId;
            await appointmentStore.UpdateAppointmentStatus(appointmentIdToUpdate, appointmentStatus);
            appointmentsViewModel.EntitiesView.Refresh();
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}


