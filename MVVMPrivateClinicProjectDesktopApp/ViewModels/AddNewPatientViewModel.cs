using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Address;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class AddNewPatientViewModel : ViewModelBase {
    private readonly IPatientRepository _patientRepository;
    private readonly IAddressRepository _addressRepository;
    
    private string _firstName = string.Empty;

    [Required(ErrorMessage = "First name is required!")]
    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Use letters only please!")]
    public string FirstName {
        get => _firstName;
        set {
            _firstName = value; 
            Validate(nameof(FirstName), value);
            SubmitCommand.RaiseCanExecuteChanged();
        }
    }

    private string _lastName = string.Empty;

    [Required(ErrorMessage = "Last name is required!")]
    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Use letters only please!")]
    public string LastName {
        get => _lastName;
        set {
            _lastName = value;
            Validate(nameof(LastName), value);
            SubmitCommand.RaiseCanExecuteChanged();
        }
    }
    
    private string _phoneNumber = string.Empty;

    [Required(AllowEmptyStrings = true)]
    [RegularExpression(@"(((\d{3}-?){3})?((\d{3}\s?){3})?(\d{9})?)?", ErrorMessage = "Invalid phone number!")]
    public string PhoneNumber {
        get => _phoneNumber;
        set {
            _phoneNumber = value;
            Validate(nameof(PhoneNumber), value);
            SubmitCommand.RaiseCanExecuteChanged();
        }
    }

    private string _email = string.Empty;

    [Required(AllowEmptyStrings = true)]
    [RegularExpression("([a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,})", ErrorMessage = "Invalid email!")]
    public string Email {
        get => _email;
        set {
            _email = value;
            Validate(nameof(Email), value);
            SubmitCommand.RaiseCanExecuteChanged();
        }
    }
    
    private string _city = string.Empty;

    [Required(ErrorMessage = "City is required!")]
    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Use letters only please!")]
    public string City {
        get => _city;
        set {
            _city = value;
            Validate(nameof(City), value);
            SubmitCommand.RaiseCanExecuteChanged();
        }
    }

    private string _postalCode = string.Empty;
    
    [Required(ErrorMessage = "Postal code is required!")]
    [RegularExpression(@"\d{2}-\d{3}", ErrorMessage = "Invalid Postal code!")]
    public string PostalCode {
        get => _postalCode;
        set {
            _postalCode = value;
            Validate(nameof(PostalCode), value);
            SubmitCommand.RaiseCanExecuteChanged();
        }
    }
    

    private string _street = string.Empty;

    [Required(ErrorMessage = "Street is required!")]
    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Use letters only please!")]
    public string Street {
        get => _street;
        set {
            _street = value;
            Validate(nameof(Street), value);
            SubmitCommand.RaiseCanExecuteChanged();
        }
    }
    

    private string _buildingNumber = string.Empty;

    [Required(ErrorMessage = "Building number is required!")]
    [RegularExpression(@"\d+[a-zA-Z]?", ErrorMessage = "Invalid building number!")]
    public string BuildingNumber {
        get => _buildingNumber;
        set {
            _buildingNumber = value;
            Validate(nameof(BuildingNumber), value);
            SubmitCommand.RaiseCanExecuteChanged();
        }
    }

    public string LocalNumber { get; set; } = string.Empty;

    public RelayCommand SubmitCommand { get; set; }
    public ICommand CreatePatientCommand { get; set; }
    
    public AddNewPatientViewModel(PatientStore patientStore){
        _patientRepository = new PatientRepository();
        _addressRepository = new AddressRepository();
        SubmitCommand = new SubmitCommand(Submit, CanSubmit);
        CreatePatientCommand = new CreatePatientCommand(this, patientStore);
    }

    private bool CanSubmit(){
        var context = new ValidationContext(this);
        var results = new List<ValidationResult>();
        return Validator.TryValidateObject(this, context, results, true);
    }

    private void Submit(){
        var saveAddressRequest = new SaveAddressRequest {
            City = City,
            PostalCode = PostalCode,
            Street = Street,
            BuildingNumber = BuildingNumber,
            LocalNumber = LocalNumber,
        };

        var savedAddress = _addressRepository.SaveAddress(saveAddressRequest);

        var savePatientRequest = new SavePatientRequest {
            FirstName = FirstName,
            LastName = LastName,
            PhoneNumber = PhoneNumber,
            Email = Email,
            AddressId = savedAddress.Id,
        };
        
        _patientRepository.SavePatient(savePatientRequest);
        ResetForm();
    }

    private void ResetForm() {
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