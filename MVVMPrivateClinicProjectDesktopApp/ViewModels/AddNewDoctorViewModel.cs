using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using static MVVMPrivateClinicProjectDesktopApp.Helpers.RegexPatterns;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class AddNewDoctorViewModel : AddNewEntityViewModelBase {
    private readonly ObservableCollection<DoctorSpecializationDto> _doctorSpecializations;
    public ICollectionView DoctorSpecializationsView { get; set; }
    
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

    [Required(ErrorMessage = "Phone number is required!")]
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
    
    private DoctorSpecializationDto _selectedSelectedDoctorSpecialization = null!;
    public DoctorSpecializationDto SelectedDoctorSpecialization {
        get => _selectedSelectedDoctorSpecialization;
        set {
            _selectedSelectedDoctorSpecialization = value;
            SubmitCommand.OnCanExecuteChanged();
            DoctorSpecialization = value.Name;
            OnPropertyChanged();
        }
    }
    
    private string _doctorSpecialization = string.Empty;

    [Required(ErrorMessage = "Doctor Specialization is required!")]
    [RegularExpression(LettersOnlyRegex, ErrorMessage = "Use letters only please!")]
    public string DoctorSpecialization
    {
        get => _doctorSpecialization;
        set
        {
            _doctorSpecialization = value;
            Validate(nameof(DoctorSpecialization), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }
    
    private ICommand LoadDoctorSpecializationsCommand { get; }
    private ICommand CreateDoctorCommand { get; }
    
    private AddNewDoctorViewModel(DoctorStore doctorStore, DoctorSpecializationStore doctorSpecializationStore){
        _doctorSpecializations = [];
        
        DoctorSpecializationsView = CollectionViewSource.GetDefaultView(_doctorSpecializations);

        LoadDoctorSpecializationsCommand = new LoadEntitiesCommand<DoctorSpecializationDto, DoctorSpecializationDto>(UpdateDoctorSpecializations, doctorSpecializationStore);
        SubmitCommand = new SubmitCommand(Submit, CanSubmit);
        CreateDoctorCommand = new CreateDoctorCommand(this, doctorStore, doctorSpecializationStore, ResetForm);
    }

    protected override void Submit(){
        CreateDoctorCommand.Execute(null);
    }

    public static AddNewDoctorViewModel LoadAddNewDoctorViewModel(DoctorStore doctorStore,
        DoctorSpecializationStore doctorSpecializationStore){
        var addNewDoctorViewModel = new AddNewDoctorViewModel(doctorStore, doctorSpecializationStore);
        
        addNewDoctorViewModel.LoadDoctorSpecializationsCommand.Execute(null);
        
        return addNewDoctorViewModel;
    }

    private void UpdateDoctorSpecializations(IEnumerable<DoctorSpecializationDto> doctorSpecializations){
        _doctorSpecializations.Clear();

        foreach (var doctorSpecialization in doctorSpecializations) {
            _doctorSpecializations.Add(doctorSpecialization);
        }
    }

    protected override void ResetForm(){
        FirstName = string.Empty;
        LastName = string.Empty;
        PhoneNumber = string.Empty;
        DoctorSpecialization = string.Empty;
    }
}