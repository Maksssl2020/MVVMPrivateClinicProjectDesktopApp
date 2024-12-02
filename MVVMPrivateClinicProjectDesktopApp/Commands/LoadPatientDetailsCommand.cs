using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadPatientDetailsCommand(PatientDetailsViewModel patientDetailsViewModel, PatientStore patientStore, AppointmentStore appointmentStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await patientStore.LoadPatientDetails();
            appointmentStore.SelectedPatientId = patientStore.PatientIdToShowDetails;
            await appointmentStore.LoadPatientAppointments();
            
            if (patientStore is { SelectedPatientData: not null, SelectedPatientAddress: not null })
                patientDetailsViewModel.UpdatePatientDetails(patientStore.SelectedPatientData,
                    patientStore.SelectedPatientAddress, appointmentStore.SelectedPatientAllAppointments);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}