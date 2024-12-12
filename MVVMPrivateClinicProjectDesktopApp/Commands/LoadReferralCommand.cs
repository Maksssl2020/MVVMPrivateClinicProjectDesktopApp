using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadReferralCommand(ReferralDetailsViewModel viewModel, ReferralStore referralStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await referralStore.LoadReferralDetails();
            viewModel.UpdateReferralDetails(referralStore.SelectedReferralDetails);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}