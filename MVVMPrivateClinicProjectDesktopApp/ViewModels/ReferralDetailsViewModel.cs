using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class ReferralDetailsViewModel : EntityDetailsViewModelBase<ReferralDetailsDto> {
    private ReferralDetailsViewModel(ReferralStore referralStore)
        :base(new LoadEntityDetailsCommand<ReferralDto, ReferralDetailsDto>(referralStore)){
    }

    public static ReferralDetailsViewModel LoadReferralDetailsViewModel(ReferralStore referralStore){
        var referralDetailsViewModel = new ReferralDetailsViewModel(referralStore);
        
        referralDetailsViewModel.LoadEntityCommand.Execute(referralDetailsViewModel);
        
        return referralDetailsViewModel;
    }
}