using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadFamilyDoctorsCommand(IssuePrescriptionViewModel viewModel, DoctorStore doctorStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await doctorStore.LoadFamilyMedicineDoctorsDto();
            viewModel.UpdateDoctorsDto(doctorStore.FamilyMedicineDoctorsDto);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}