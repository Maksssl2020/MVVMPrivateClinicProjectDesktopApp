using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadReferralTestCommand(ReferralTestStore referralTestStore): AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            if (parameter is ReferralTestDetailsViewModel viewModel) {
                await referralTestStore.LoadReferralTest();
                viewModel.Entity = referralTestStore.ReferralTestDetails;
            }
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}