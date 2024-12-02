using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadAppointmentsCommand(AppointmentsViewModel appointmentsViewModel, AppointmentStore appointmentStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await appointmentStore.LoadAppointments();
            appointmentsViewModel.UpdateAppointments(appointmentStore.AllAppointments);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}