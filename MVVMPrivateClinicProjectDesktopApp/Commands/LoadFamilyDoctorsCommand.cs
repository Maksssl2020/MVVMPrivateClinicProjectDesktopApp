using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadFamilyDoctorsCommand(Action<IEnumerable<DoctorDto>> updateFamlilyDoctors, DoctorStore doctorStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await doctorStore.LoadFamilyMedicineDoctorsDto();
            updateFamlilyDoctors.Invoke(doctorStore.FamilyMedicineDoctorsDto);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}