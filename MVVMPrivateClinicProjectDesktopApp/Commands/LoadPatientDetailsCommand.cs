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
            appointmentStore.SelectedPatientId = patientStore.PatientIdToShowDetails;
            prescriptionStore.SelectedPatientId = patientStore.PatientIdToShowDetails;
            referralStore.SelectedPatientId = patientStore.PatientIdToShowDetails;
            diagnosisStore.SelectedPatientId = patientStore.PatientIdToShowDetails;
            
            await patientStore.LoadPatientDetails();
            await appointmentStore.LoadPatientAppointments();
            await prescriptionStore.LoadPatientPrescriptions();
            await referralStore.LoadPatientReferrals();
            await diagnosisStore.LoadPatientDiagnoses();

            if (patientStore is { SelectedPatientData: not null, SelectedPatientAddress: not null })
                patientDetailsViewModel.UpdatePatientDetails(patientStore.SelectedPatientData,
                    patientStore.SelectedPatientAddress, appointmentStore.SelectedPatientAllAppointments,
                    prescriptionStore.SelectedPatientPrescriptionsDto, referralStore.SelectedPatientReferralsDto,
                    diagnosisStore.SelectedPatientDiagnoses);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}