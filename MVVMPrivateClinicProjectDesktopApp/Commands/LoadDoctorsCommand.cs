using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadDoctorsCommand(IDoctorsViewModel viewModel, DoctorStore doctorStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await doctorStore.LoadAllDoctorsDto();
            viewModel.UpdateDoctorsDto(doctorStore.AllDoctorsDto);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}