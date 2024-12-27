using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class GenerateReferralPdfCommand(ReferralStore referralStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            if (parameter is int referralId) {
                await referralStore.GenerateReferralDetailsPdf(referralId);
            }
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}