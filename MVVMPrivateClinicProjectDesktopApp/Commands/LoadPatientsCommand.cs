using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadPatientsCommand(IPatientViewModel viewModel, PatientStore patientStore)
    : AsyncRelayCommand {
    
    public override async Task ExecuteAsync(object? parameter){
        try {
            await patientStore.LoadPatients();
            viewModel.UpdatePatients(patientStore.Patients);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}