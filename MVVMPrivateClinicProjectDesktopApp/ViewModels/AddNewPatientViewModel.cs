using System.ComponentModel.DataAnnotations;
using System.Windows;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Address;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class AddNewPatientViewModel : ViewModelBase {
    private IPatientRepository _patientRepository;
    private IAddressRepository _addressRepository;
    
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
    
    public SubmitCommand SubmitCommand { get; set; }

    public AddNewPatientViewModel(){
        SubmitCommand = new SubmitCommand(Submit, CanSubmit);
    }

    private bool CanSubmit(){
        var context = new ValidationContext(this);
        var results = new List<ValidationResult>();
        return Validator.TryValidateObject(this, context, results, true);
    }

    private void Submit(){
        // new SaveAddressRequest {
        //  City = City,
        //  PostalCode = PostalCode,
        //  Street = Street,
        //  BuildingNumber = BuildingNumber,
        //  LocalNumber = LocalN
        // }
        // new SavePatientRequest();
    }
}