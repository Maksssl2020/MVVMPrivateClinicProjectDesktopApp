using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class CreateReferralTestCommand(AddNewReferralTestViewModel viewModel, ReferralTestStore referralTestStore, Action resetForm) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            var saveReferralTestRequest = new SaveReferralTestRequest {
                ReferralTestName = viewModel.ReferralTestName,
                ReferralTestDescription = viewModel.ReferralTestDescription,
            };

            await referralTestStore.CreateEntity(saveReferralTestRequest);
            resetForm.Invoke();
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}