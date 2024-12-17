using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadReferralTestsCommand(Action<IEnumerable<ReferralTestDto>> updateEntities, ReferralTestStore referralTestStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await referralTestStore.LoadReferralTests();
            updateEntities.Invoke(referralTestStore.ReferralTests);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}