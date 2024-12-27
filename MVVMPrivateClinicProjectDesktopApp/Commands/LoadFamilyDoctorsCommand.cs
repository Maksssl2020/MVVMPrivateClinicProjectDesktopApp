using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadFamilyDoctorsCommand(Action<IEnumerable<DoctorDto>> updateFamilyDoctors, DoctorStore doctorStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await doctorStore.LoadFamilyMedicineDoctorsDto();
            updateFamilyDoctors.Invoke(doctorStore.FamilyMedicineDoctorsDto);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}