using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class ReferralTestsViewModel : ViewModelBase, IReferralTestsViewModel {
    
    private readonly ObservableCollection<ReferralTestDto> _referralTests;
    public ICollectionView ReferralTestsView { get; set; }

    private ICommand LoadReferralTestsCommand { get; set; }

    private ReferralTestsViewModel(ReferralTestStore referralTestStore){
        _referralTests = [];
        
        ReferralTestsView = CollectionViewSource.GetDefaultView(_referralTests);

        LoadReferralTestsCommand = new LoadReferralTestsCommand(this, referralTestStore);
    }

    public static ReferralTestsViewModel LoadReferralTestsViewModel(ReferralTestStore referralTestStore){
        var referralTestsViewModel = new ReferralTestsViewModel(referralTestStore);
        
        referralTestsViewModel.LoadReferralTestsCommand.Execute(null);
        
        return referralTestsViewModel;
    }
    
    public void UpdateReferralTests(IEnumerable<ReferralTestDto> referralTests){
        _referralTests.Clear();

        foreach (var referralTest in referralTests) {
            _referralTests.Add(referralTest);
        }
    }
}