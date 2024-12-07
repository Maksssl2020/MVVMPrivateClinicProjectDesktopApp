using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class AddNewPatientNoteViewModel : ViewModelBase, IDoctorsViewModel {
    public static string Today => DateTime.Today.ToString("dd-MM-yyyy");

    private readonly ObservableCollection<DoctorDto> _doctorsDto;
    public ICollectionView DoctorsDtoView { get; set; }

    private DoctorDto _selectedDoctor = null!;

    [Required(ErrorMessage = "Doctor is required!")]
    public DoctorDto SelectedDoctor {
        get => _selectedDoctor;
        set {
            _selectedDoctor = value;
            Validate(nameof(SelectedDoctor), value);
            SubmitCommand.OnCanExecuteChanged();
        }
    }
    
    private string _patientNoteDescription = string.Empty;

    [Required(ErrorMessage = "Patient Note is required!")]
    public string PatientNoteDescription {
        get => _patientNoteDescription;
        set {
            _patientNoteDescription = value;
            Validate(nameof(PatientNoteDescription), value);
            SubmitCommand.OnCanExecuteChanged();
        }
    }

    private int _patientId;

    public int SelectedPatientId {
        get => _patientId;
        set {
            _patientId = value;
            OnPropertyChanged();
        }
    }
    
    private ICommand LoadDoctorsCommand { get; set; }
    public SubmitCommand SubmitCommand { get; set; }
    private ICommand CreatePatientNoteCommand { get; set; }

    private AddNewPatientNoteViewModel(DoctorStore doctorStore, PatientStore patientStore, PatientNoteStore patientNoteStore){
        _doctorsDto = [];
        SelectedPatientId = patientStore.PatientIdToShowDetails;
        DoctorsDtoView = CollectionViewSource.GetDefaultView(_doctorsDto);
        
        SubmitCommand = new SubmitCommand(Submit, CanSubmit);
        LoadDoctorsCommand = new LoadFamilyDoctorsCommand(this, doctorStore);
        CreatePatientNoteCommand = new CreatePatientNoteCommand(this, patientNoteStore, ResetForm);
    }
    
    public static AddNewPatientNoteViewModel LoadAddNewPatientNoteViewModel(DoctorStore doctorStore, PatientStore patientStore,
        PatientNoteStore patientNoteStore){
        var addNewPatientNoteViewModel = new AddNewPatientNoteViewModel(doctorStore, patientStore, patientNoteStore);
        
        addNewPatientNoteViewModel.LoadDoctorsCommand.Execute(null);
        
        return addNewPatientNoteViewModel;
    }
    
    private bool CanSubmit(){
        var context = new ValidationContext(this);
        var results = new List<ValidationResult>();
        return Validator.TryValidateObject(this, context, results, true);
    }

    private void Submit(){
        CreatePatientNoteCommand.Execute(null);
    }

    private void ResetForm(){
        PatientNoteDescription = string.Empty;
        SelectedDoctor = null!;
    }
    
    public void UpdateDoctorsDto(IEnumerable<DoctorDto> doctorsDto){
        _doctorsDto.Clear();
        
        foreach (var doctorDto in doctorsDto) {
            _doctorsDto.Add(doctorDto);
        }
    }
}