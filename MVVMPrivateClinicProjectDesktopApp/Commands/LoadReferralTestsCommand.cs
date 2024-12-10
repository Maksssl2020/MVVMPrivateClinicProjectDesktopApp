using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadReferralTestsCommand(IReferralTestsViewModel viewModel, ReferralTestStore referralTestStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await referralTestStore.LoadReferralTests();
            viewModel.UpdateReferralTests(referralTestStore.ReferralTests);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}