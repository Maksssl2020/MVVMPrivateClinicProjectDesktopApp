using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class ReferralTestDetailsViewModel : EntityDetailsViewModelBase<ReferralTestDetailsDto> {
    private ReferralTestDetailsViewModel(ReferralTestStore referralTestStore) : 
        base(new LoadReferralTestCommand(referralTestStore)) {
    }

    public static ReferralTestDetailsViewModel LoadReferralTestDetailsViewModel(ReferralTestStore referralTestStore){
        var referralTestDetailsViewModel = new ReferralTestDetailsViewModel(referralTestStore);
        
        referralTestDetailsViewModel.LoadEntityCommand.Execute(referralTestDetailsViewModel);
        
        return referralTestDetailsViewModel;
    }
}
