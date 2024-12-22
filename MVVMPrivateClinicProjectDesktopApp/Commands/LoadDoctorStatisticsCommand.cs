using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadDoctorStatisticsCommand(DoctorDetailsViewModel doctorDetailsViewModel, DoctorStore doctorStore): AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await doctorStore.LoadDoctorStatistics();
            doctorDetailsViewModel.DoctorStatistics = doctorStore.DoctorStatisticsDto;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}