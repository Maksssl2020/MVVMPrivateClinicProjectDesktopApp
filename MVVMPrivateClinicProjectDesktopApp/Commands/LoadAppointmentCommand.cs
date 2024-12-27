using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadAppointmentCommand(AppointmentStore appointmentStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            if (parameter is AppointmentDetailsViewModel viewModel) {
                await appointmentStore.LoadEntityDetails();
                viewModel.Entity = appointmentStore.SelectedEntityDetails;
            }
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}