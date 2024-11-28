using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadPatientsCommand(PatientsViewModel patientsViewModel, PatientStore patientStore)
    : AsyncRelayCommand {
    
    public override async Task ExecuteAsync(object? parameter){
        try
        {
            await patientStore.LoadPatients();
            patientsViewModel.UpdatePatients(patientStore.Patients);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}