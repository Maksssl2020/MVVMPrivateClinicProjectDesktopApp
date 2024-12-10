using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class ReferralsViewModel : ViewModelBase {
    private readonly ObservableCollection<ReferralDto> _referralsDto;
    public ICollectionView ReferralsView { get; set; }

    private ICommand LoadReferralsCommand { get; set; }

    private ReferralsViewModel(ReferralStore referralStore){
        _referralsDto = [];
        ReferralsView = CollectionViewSource.GetDefaultView(_referralsDto);

        LoadReferralsCommand = new LoadReferralsDtoCommand(this, referralStore);
        referralStore.ReferralCreated += OnReferralCreated;
    }

    public static ReferralsViewModel LoadReferralsViewModel(ReferralStore referralStore){
        var referralsViewModel = new ReferralsViewModel(referralStore);
        
        referralsViewModel.LoadReferralsCommand.Execute(null);
        
        return referralsViewModel;
    }

    private void OnReferralCreated(ReferralDto referralDto){
        _referralsDto.Add(referralDto);
    }
    
    public void UpdateReferrals(IEnumerable<ReferralDto> referrals){
        _referralsDto.Clear();

        foreach (var referral in referrals) {
            _referralsDto.Add(referral);
        }
    }
}