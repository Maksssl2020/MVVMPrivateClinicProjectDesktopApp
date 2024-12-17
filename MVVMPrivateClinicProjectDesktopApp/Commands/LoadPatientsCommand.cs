using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadPatientsCommand(Action<IEnumerable<PatientDto>> updatePatients , PatientStore patientStore)
    : AsyncRelayCommand {
    
    public override async Task ExecuteAsync(object? parameter){
        try {
            await patientStore.LoadPatients();
            updatePatients.Invoke(patientStore.Patients);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}