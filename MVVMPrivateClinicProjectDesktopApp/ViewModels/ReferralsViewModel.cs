using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class ReferralsViewModel : ViewModelBase {
    private readonly ReferralStore _referralStore;
    private readonly AddSpecificDataToPatientStore _addSpecificDataToPatientStore;
    
    private readonly ObservableCollection<ReferralDto> _referralsDto;
    public ICollectionView ReferralsView { get; set; }

    private ICommand LoadReferralsCommand { get; set; }
    public ICommand ShowReferralDetailsCommand { get; set; }
    public ICommand ShowSelectPatientToAddSpecificDataModal { get; set; }


    private ReferralsViewModel(ReferralStore referralStore, AddSpecificDataToPatientStore addSpecificDataToPatientStore, ModalNavigationViewModel modalNavigationViewModel){
        _referralStore = referralStore;
        _addSpecificDataToPatientStore = addSpecificDataToPatientStore;
        
        _referralsDto = [];
        ReferralsView = CollectionViewSource.GetDefaultView(_referralsDto);

        LoadReferralsCommand = new LoadReferralsDtoCommand(this, referralStore);
        ShowReferralDetailsCommand = modalNavigationViewModel.ShowReferralDetailsModal;
        ShowSelectPatientToAddSpecificDataModal = modalNavigationViewModel.ShowSelectPatientToAddSpecificDataModal;

        referralStore.ReferralCreated += OnReferralCreated;
    }

    public static ReferralsViewModel LoadReferralsViewModel(ReferralStore referralStore, AddSpecificDataToPatientStore addSpecificDataToPatientStore, ModalNavigationViewModel modalNavigationViewModel){
        var referralsViewModel = new ReferralsViewModel(referralStore, addSpecificDataToPatientStore, modalNavigationViewModel);
        
        referralsViewModel.LoadReferralsCommand.Execute(null);
        
        return referralsViewModel;
    }
    
    public void SetDataInAddSpecificDataToPatientStore(){
        _addSpecificDataToPatientStore.DataToAddName = "Referral";
        _addSpecificDataToPatientStore.DataColor =
            (SolidColorBrush) Application.Current.Resources["CustomYellowColor1"]!;
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

    public void SetReferralIdToShowDetails(int referralId){
        _referralStore.SelectedReferralId = referralId;
    }
}