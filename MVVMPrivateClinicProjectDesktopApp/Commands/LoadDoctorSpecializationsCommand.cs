using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadDoctorSpecializationsCommand(AddNewDoctorViewModel viewModel, DoctorSpecializationStore doctorSpecializationStore): AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await doctorSpecializationStore.LoadDoctorSpecializations();
            viewModel.UpdateDoctorSpecializations(doctorSpecializationStore.DoctorSpecializations);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}