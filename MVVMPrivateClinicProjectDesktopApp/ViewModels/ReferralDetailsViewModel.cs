using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class ReferralDetailsViewModel : ViewModelBase {

    private ReferralDetailsDto _referralDetails = null!;
    public ReferralDetailsDto ReferralDetails {
        get => _referralDetails;
        set {
            _referralDetails = value;
            OnPropertyChanged();
        }
    }

    private ICommand LoadReferralCommand { get; set; }

    private ReferralDetailsViewModel(ReferralStore referralStore){
        LoadReferralCommand = new LoadReferralCommand(this, referralStore);
    }

    public static ReferralDetailsViewModel LoadReferralDetailsViewModel(ReferralStore referralStore){
        var referralDetailsViewModel = new ReferralDetailsViewModel(referralStore);
        
        referralDetailsViewModel.LoadReferralCommand.Execute(null);
        
        return referralDetailsViewModel;
    }
    
    public void UpdateReferralDetails(ReferralDetailsDto referralDetails){
        ReferralDetails = referralDetails;
    }
}