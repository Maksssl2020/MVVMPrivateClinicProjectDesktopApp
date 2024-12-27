using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadPatientDetailsCommand(
    PatientDetailsViewModel patientDetailsViewModel,
    PatientStore patientStore,
    AppointmentStore appointmentStore,
    PrescriptionStore prescriptionStore,
    ReferralStore referralStore,
    DiagnosisStore diagnosisStore
    ) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            appointmentStore.SelectedPatientId = patientStore.EntityIdToShowDetails;
            prescriptionStore.SelectedPatientId = patientStore.EntityIdToShowDetails;
            referralStore.SelectedPatientId = patientStore.EntityIdToShowDetails;
            diagnosisStore.SelectedPatientId = patientStore.EntityIdToShowDetails;
            
            await patientStore.LoadPatientDetails();
            await appointmentStore.LoadPatientAppointments();
            await prescriptionStore.LoadPatientPrescriptions();
            await referralStore.LoadPatientReferrals();
            await diagnosisStore.LoadPatientDiagnoses();

            if (patientStore is { SelectedEntityDetails: not null, SelectedPatientAddress: not null })
                patientDetailsViewModel.UpdatePatientDetails(patientStore.SelectedEntityDetails,
                    patientStore.SelectedPatientAddress, appointmentStore.SelectedPatientAppointments,
                    prescriptionStore.SelectedPatientPrescriptionsDto, referralStore.SelectedPatientReferralsDto,
                    diagnosisStore.SelectedPatientDiagnoses);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}