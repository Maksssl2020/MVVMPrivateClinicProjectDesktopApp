using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class CreateReferralCommand(IssueReferralViewModel viewModel, ReferralStore referralStore, Action resetForm): AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            var saveReferralRequest = new SaveReferralRequest {
                Name = viewModel.ReferralName,
                Description = viewModel.ReferralDescription,
                DiseaseId = viewModel.SelectedDisease.Id,
                DoctorId = viewModel.SelectedDoctor.Id,
                ReferralTestId = viewModel.SelectedReferralTest.Id,
                PatientId = viewModel.SelectedPatientId
            };
            
            await referralStore.CreateEntity(saveReferralRequest);
            resetForm.Invoke();
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}