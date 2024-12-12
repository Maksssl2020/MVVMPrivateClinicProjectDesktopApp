using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadPatientCommand(PatientDataModalViewModel patientDataModalViewModel, PatientStore patientStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await patientStore.LoadPatientData();
            if (patientStore.SelectedPatientData != null) {
                patientDataModalViewModel.SelectedPatient = patientStore.SelectedPatientData;
            }
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}