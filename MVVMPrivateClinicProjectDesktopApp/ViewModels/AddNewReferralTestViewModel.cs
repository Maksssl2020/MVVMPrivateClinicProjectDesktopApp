using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using static MVVMPrivateClinicProjectDesktopApp.Helpers.RegexPatterns;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class AddNewReferralTestViewModel : AddNewEntityViewModelBase {
    private string _referralTestName = string.Empty;

    [Required(ErrorMessage = "Referral Test Name is required!")]
    public string ReferralTestName {
        get => _referralTestName;
        set {
            _referralTestName = value;
            Validate(nameof(ReferralTestName), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }
    
    private string _referralTestDescription = string.Empty;
    
    [Required(ErrorMessage = "Referral Test Description is required!")]
    public string ReferralTestDescription {
        get => _referralTestDescription;
        set {
            _referralTestDescription = value;
            Validate(nameof(ReferralTestDescription), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }

    private ICommand CreateReferralTestCommand { get; set; }

    public AddNewReferralTestViewModel(ReferralTestStore referralTestStore){
        CreateReferralTestCommand = new CreateReferralTestCommand(this, referralTestStore, ResetForm);
    }

    protected override void Submit(){
        CreateReferralTestCommand.Execute(null);
    }

    protected override void ResetForm(){
        ReferralTestName = string.Empty;
        ReferralTestDescription = string.Empty;
    }
}