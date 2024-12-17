using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class AddNewPatientNoteViewModel : AddNewEntityViewModelBase {
    public static string Today => DateTime.Today.ToString("dd-MM-yyyy");

    private readonly ObservableCollection<DoctorDto> _doctorsDto;
    private readonly ObservableCollection<PatientNoteWithDoctorDataDto> _patientNotesDto;
    public ICollectionView DoctorsDtoView { get; set; }
    public ICollectionView PatientNotesWithDoctorDataDtoView { get; set; }

    private DoctorDto _selectedDoctor = null!;
    [Required(ErrorMessage = "Doctor is required!")]
    public DoctorDto SelectedDoctor {
        get => _selectedDoctor;
        set {
            _selectedDoctor = value;
            Validate(nameof(SelectedDoctor), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }
    
    private string _patientNoteDescription = string.Empty;

    [Required(ErrorMessage = "Patient Note is required!")]
    [RegularExpression(@"([\p{L} .,!?]+[\s]?)+", ErrorMessage = "Use letters only please!")]
    public string PatientNoteDescription {
        get => _patientNoteDescription;
        set {
            _patientNoteDescription = value;
            Validate(nameof(PatientNoteDescription), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }

    private readonly int _patientId;
    public int SelectedPatientId {
        get => _patientId;
        private init {
            _patientId = value;
            OnPropertyChanged();
        }
    }
    
    private ICommand LoadDoctorsCommand { get; set; }
    private ICommand LoadPatientNotesCommand { get; set; }
    private ICommand CreatePatientNoteCommand { get; set; }

    private AddNewPatientNoteViewModel(DoctorStore doctorStore, PatientStore patientStore, PatientNoteStore patientNoteStore){
        _doctorsDto = [];
        _patientNotesDto = [];
        SelectedPatientId = patientStore.PatientIdToShowDetails;
        DoctorsDtoView = CollectionViewSource.GetDefaultView(_doctorsDto);
        PatientNotesWithDoctorDataDtoView = CollectionViewSource.GetDefaultView(_patientNotesDto);
        
        LoadDoctorsCommand = new LoadFamilyDoctorsCommand(UpdateDoctorsDto, doctorStore);
        LoadPatientNotesCommand = new LoadSelectedPatientNotesDtoCommand(this, patientNoteStore, patientStore);
        CreatePatientNoteCommand = new CreatePatientNoteCommand(this, patientNoteStore, ResetForm);
    }
    
    public static AddNewPatientNoteViewModel LoadAddNewPatientNoteViewModel(
        DoctorStore doctorStore, 
        PatientStore patientStore,
        PatientNoteStore patientNoteStore
        ){
        var addNewPatientNoteViewModel = new AddNewPatientNoteViewModel(doctorStore, patientStore, patientNoteStore);
        
        addNewPatientNoteViewModel.LoadDoctorsCommand.Execute(null);
        addNewPatientNoteViewModel.LoadPatientNotesCommand.Execute(null);
        
        return addNewPatientNoteViewModel;
    }
    
   protected override void Submit(){
        CreatePatientNoteCommand.Execute(null);
    }

    protected override void ResetForm(){
        PatientNoteDescription = string.Empty;
        SelectedDoctor = null!;
    }
    
    public void UpdateDoctorsDto(IEnumerable<DoctorDto> doctorsDto){
        _doctorsDto.Clear();
        
        foreach (var doctorDto in doctorsDto) {
            _doctorsDto.Add(doctorDto);
        }
    }

    public void UpdateSelectedPatientNotesDto(IEnumerable<PatientNoteWithDoctorDataDto> selectedPatientNotes){
        _patientNotesDto.Clear();

        foreach (var patientNoteDto in selectedPatientNotes) {
            _patientNotesDto.Add(patientNoteDto);
        }
    }
}