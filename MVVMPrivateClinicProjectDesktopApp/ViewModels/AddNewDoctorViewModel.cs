using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class AddNewDoctorViewModel : ViewModelBase {
    private readonly ObservableCollection<DoctorSpecializationDto> _doctorSpecializations;
    public ICollectionView DoctorSpecializationsView { get; set; }
    
    private string _firstName = string.Empty;

    [Required(ErrorMessage = "First name is required!")]
    [RegularExpression(@"([\p{L}]+[\s]?)+", ErrorMessage = "Use letters only please!")]
    public string FirstName {
        get => _firstName;
        set {
            _firstName = value;
            Validate(nameof(FirstName), value);
            SubmitCommand.OnCanExecuteChanged();
        }
    }
    
    private string _lastName = string.Empty;

    [Required(ErrorMessage = "Last name is required!")]
    [RegularExpression(@"([\p{L}]+[\s]?)+", ErrorMessage = "Use letters only please!")]
    public string LastName {
        get => _lastName;
        set {
            _lastName = value;
            Validate(nameof(LastName), value);
            SubmitCommand.OnCanExecuteChanged();
        }
    }
    
    private string _phoneNumber = string.Empty;

    [Required(ErrorMessage = "Phone number is required!")]
    [RegularExpression(@"(((\d{3}-?){3})?((\d{3}\s?){3})?(\d{9})?)?", ErrorMessage = "Invalid phone number!")]
    public string PhoneNumber {
        get => _phoneNumber;
        set {
            _phoneNumber = value;
            Validate(nameof(PhoneNumber), value);
            SubmitCommand.OnCanExecuteChanged();
        }
    }
    
    private DoctorSpecializationDto _selectedSelectedDoctorSpecialization = null!;

    public DoctorSpecializationDto SelectedDoctorSpecialization {
        get => _selectedSelectedDoctorSpecialization;
        set {
            _selectedSelectedDoctorSpecialization = value;
            SubmitCommand.OnCanExecuteChanged();
            DoctorSpecialization = value.Name;
        }
    }
    
    private string _doctorSpecialization = string.Empty;

    [Required(ErrorMessage = "Doctor Specialization is required!")]
    [RegularExpression(@"([\p{L}]+[\s]?)+", ErrorMessage = "Use letters only please!")]
    public string DoctorSpecialization
    {
        get => _doctorSpecialization;
        set
        {
            _doctorSpecialization = value;
            Validate(nameof(DoctorSpecialization), value);
            SubmitCommand.OnCanExecuteChanged();
        }
    }
    
    private ICommand LoadDoctorSpecializationsCommand { get; set; }
    public SubmitCommand SubmitCommand { get; set; }
    public ICommand CreateDoctorCommand { get; set; }
    
    private AddNewDoctorViewModel(DoctorStore doctorStore, DoctorSpecializationStore doctorSpecializationStore){
        _doctorSpecializations = [];
        
        DoctorSpecializationsView = CollectionViewSource.GetDefaultView(_doctorSpecializations);

        LoadDoctorSpecializationsCommand = new LoadDoctorSpecializationsCommand(this, doctorSpecializationStore);
        SubmitCommand = new SubmitCommand(Submit, CanSubmit);
        CreateDoctorCommand = new CreateDoctorCommand(this, doctorStore, doctorSpecializationStore, ResetForm);
    }

    private bool CanSubmit(){
        var context = new ValidationContext(this);
        var results = new List<ValidationResult>();
        return Validator.TryValidateObject(this, context, results, true);
    }

    private void Submit(){
        CreateDoctorCommand.Execute(null);
    }

    public static AddNewDoctorViewModel LoadAddNewDoctorViewModel(DoctorStore doctorStore,
        DoctorSpecializationStore doctorSpecializationStore){
        var addNewDoctorViewModel = new AddNewDoctorViewModel(doctorStore, doctorSpecializationStore);
        
        addNewDoctorViewModel.LoadDoctorSpecializationsCommand.Execute(null);
        
        return addNewDoctorViewModel;
    }
    
    public void UpdateDoctorSpecializations(IEnumerable<DoctorSpecializationDto> doctorSpecializations){
        _doctorSpecializations.Clear();

        foreach (var doctorSpecialization in doctorSpecializations) {
            _doctorSpecializations.Add(doctorSpecialization);
        }
    }

    private void ResetForm(){
        FirstName = string.Empty;
        LastName = string.Empty;
        PhoneNumber = string.Empty;
        DoctorSpecialization = string.Empty;
    }
}