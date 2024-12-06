using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadReferralsDtoCommand(ReferralsViewModel viewModel, ReferralStore referralStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await referralStore.LoadReferrals();
            viewModel.UpdateReferrals(referralStore.ReferralsDto);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}