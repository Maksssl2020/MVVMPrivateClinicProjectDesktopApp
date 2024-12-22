using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadDoctorAppointments(DoctorDetailsViewModel viewModel, AppointmentStore appointmentStore): AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            appointmentStore.SelectedDoctorId = viewModel.SelectedDoctorId;
            await appointmentStore.LoadDoctorAppointments();
            viewModel.UpdateAppointments(appointmentStore.SelectedDoctorAppointments);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}