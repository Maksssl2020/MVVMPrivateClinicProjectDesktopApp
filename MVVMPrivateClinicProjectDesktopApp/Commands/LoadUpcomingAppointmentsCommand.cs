using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadUpcomingAppointmentsCommand(HomeViewModel viewModel, AppointmentStore appointmentStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await appointmentStore.LoadUpcomingAppointments();
            viewModel.UpdateUpcomingAppointments(appointmentStore.UpcomingAppointments);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}