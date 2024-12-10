using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class CreatePrescriptionCommand(IssuePrescriptionViewModel viewModel, PrescriptionStore prescriptionStore, Action resetForm) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        var savePrescriptionRequest = new SavePrescriptionRequest {
            IdDoctor = viewModel.SelectedDoctor.Id,
            IdPatient = viewModel.SelectedPatientId,
            PrescriptionDescription = viewModel.PrescriptionDescription,
            PrescriptionValidity = viewModel.PrescriptionValidity,
            SelectedMedicines = viewModel.SelectedMedicines
        };

        await prescriptionStore.CreatePrescription(savePrescriptionRequest);
        resetForm.Invoke();
    }
}