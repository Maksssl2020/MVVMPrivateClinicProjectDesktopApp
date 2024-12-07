using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadDoctorsCommand(DoctorsViewModel viewModel, DoctorStore doctorStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await doctorStore.LoadAllDoctorsDto();
            viewModel.UpdateDoctors(doctorStore.AllDoctorsDto);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}