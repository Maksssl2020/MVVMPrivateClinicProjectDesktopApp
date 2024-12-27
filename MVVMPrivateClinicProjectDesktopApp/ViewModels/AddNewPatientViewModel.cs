using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using static MVVMPrivateClinicProjectDesktopApp.Helpers.RegexPatterns;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class AddNewPatientViewModel : AddNewEntityViewModelBase {
    private string _firstName = string.Empty;

    [Required(ErrorMessage = "First name is required!")]
    [RegularExpression(LettersOnlyRegex, ErrorMessage = "Use letters only please!")]
    public string FirstName {
        get => _firstName;
        set {
            _firstName = value; 
            Validate(nameof(FirstName), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }

    private string _lastName = string.Empty;

    [Required(ErrorMessage = "Last name is required!")]
    [RegularExpression(LettersOnlyRegex, ErrorMessage = "Use letters only please!")]
    public string LastName {
        get => _lastName;
        set {
            _lastName = value;
            Validate(nameof(LastName), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }
    
    private string _phoneNumber = string.Empty;

    [Required(AllowEmptyStrings = true)]
    [RegularExpression(PhoneNumberRegex, ErrorMessage = "Invalid phone number!")]
    public string PhoneNumber {
        get => _phoneNumber;
        set {
            _phoneNumber = value;
            Validate(nameof(PhoneNumber), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }

    private string _email = string.Empty;

    [Required(AllowEmptyStrings = true)]
    [RegularExpression(EmailAddressRegex, ErrorMessage = "Invalid email!")]
    public string Email {
        get => _email;
        set {
            _email = value;
            Validate(nameof(Email), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }
    
    private string _city = string.Empty;

    [Required(ErrorMessage = "City is required!")]
    [RegularExpression(LettersOnlyRegexWithAdditionalCharacters, ErrorMessage = "Use letters only please!")]
    public string City {
        get => _city;
        set {
            _city = value;
            Validate(nameof(City), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }

    private string _postalCode = string.Empty;
    
    [Required(ErrorMessage = "Postal code is required!")]
    [RegularExpression(PostalCodeRegex, ErrorMessage = "Invalid Postal code!")]
    public string PostalCode {
        get => _postalCode;
        set {
            _postalCode = value;
            Validate(nameof(PostalCode), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }
    

    private string _street = string.Empty;

    [Required(ErrorMessage = "Street is required!")]
    [RegularExpression(LettersOnlyRegexWithAdditionalCharacters, ErrorMessage = "Use letters only please!")]
    public string Street {
        get => _street;
        set {
            _street = value;
            Validate(nameof(Street), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }
    

    private string _buildingNumber = string.Empty;

    [Required(ErrorMessage = "Building number is required!")]
    [RegularExpression(BuildingNumberRegex, ErrorMessage = "Invalid building number!")]
    public string BuildingNumber {
        get => _buildingNumber;
        set {
            _buildingNumber = value;
            Validate(nameof(BuildingNumber), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }

    public string LocalNumber { get; set; } = string.Empty;

    private ICommand CreatePatientCommand { get; set; }
    
    public AddNewPatientViewModel(PatientStore patientStore){
        CreatePatientCommand = new CreatePatientCommand(this, patientStore, ResetForm);
    }

    protected override void Submit(){
        CreatePatientCommand.Execute(null);
    }

    protected override void ResetForm() {
        FirstName = string.Empty;
        LastName = string.Empty;
        PhoneNumber = string.Empty;
        Email = string.Empty;
        City = string.Empty;
        PostalCode = string.Empty;
        Street = string.Empty;
        BuildingNumber = string.Empty;
        LocalNumber = string.Empty;
    }
}