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

public class AddNewDiagnosisViewModel : AddNewEntityViewModelBase {
    public static string Today => DateTime.Now.ToString("dd-MM-yyyy");
    
    private readonly ObservableCollection<DoctorDto> _doctors;
    private readonly ObservableCollection<DiseaseDto> _diseases;

    public ICollectionView DoctorsView { get; set; }
    public ICollectionView DiseasesView { get; set; }

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

    private string _diagnosisDescription = string.Empty;

    [Required(ErrorMessage = "Diagnosis is required!")]
    public string DiagnosisDescription {
        get => _diagnosisDescription;
        set {
            _diagnosisDescription = value;
            Validate(nameof(DiagnosisDescription), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }
    
    private DiseaseDto _selectedDisease = null!;

    [Required(ErrorMessage = "Disease is required!")]
    public DiseaseDto SelectedDisease {
        get => _selectedDisease;
        set {
            _selectedDisease  = value;
            Validate(nameof(SelectedDisease), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }

    public int SelectedPatientId { get; set; }
    
    private ICommand LoadDoctorsCommand { get; set; }
    private ICommand LoadDiseasesCommand { get; set; }
    private ICommand CreateDiagnosisCommand { get; set; }

    private AddNewDiagnosisViewModel(PatientStore patientStore, DiagnosisStore diagnosisStore, DoctorStore doctorStore, DiseaseStore diseaseStore){
        _doctors = [];
        _diseases = [];
        SelectedPatientId = patientStore.EntityIdToShowDetails;
        
        DoctorsView = CollectionViewSource.GetDefaultView(_doctors);
        DiseasesView = CollectionViewSource.GetDefaultView(_diseases);

        LoadDoctorsCommand = new LoadEntitiesCommand<DoctorDto, DoctorStatisticsDto>(UpdateDoctorsDto, doctorStore);
        LoadDiseasesCommand = new LoadEntitiesCommand<DiseaseDto, DiseaseDetailsDto>(UpdateDiseasesDto, diseaseStore);
        SubmitCommand = new SubmitCommand(Submit, CanSubmit);
        CreateDiagnosisCommand = new CreateDiagnosisCommand(this, diagnosisStore, ResetForm);
    }

    public static AddNewDiagnosisViewModel LoadAddDiagnosisViewModel(PatientStore patientStore, DiagnosisStore diagnosisStore, DoctorStore doctorStore, DiseaseStore diseaseStore){
        var addDiagnosisViewModel = new AddNewDiagnosisViewModel(patientStore, diagnosisStore, doctorStore, diseaseStore);
        
        addDiagnosisViewModel.LoadDoctorsCommand.Execute(null);
        addDiagnosisViewModel.LoadDiseasesCommand.Execute(null);
        
        return addDiagnosisViewModel;
    }

    protected override void Submit(){
        CreateDiagnosisCommand.Execute(null);
    }

    private void UpdateDoctorsDto(IEnumerable<DoctorDto> doctorsDto){
        _doctors.Clear();

        foreach (var doctorDto in doctorsDto) {
            _doctors.Add(doctorDto);
        }
    }

    public void UpdateDiseasesDto(IEnumerable<DiseaseDto> diseaseDtos){
        _diseases.Clear();

        foreach (var diseaseDto in diseaseDtos) {
            _diseases.Add(diseaseDto);
        }
    }

    protected override void ResetForm(){
        SelectedDoctor = null!;
        SelectedDisease = null!;
        DiagnosisDescription = string.Empty;
    }
}