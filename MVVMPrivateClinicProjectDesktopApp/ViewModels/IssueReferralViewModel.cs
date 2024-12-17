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

public class IssueReferralViewModel : AddNewEntityViewModelBase {
    public static string Today => DateTime.Today.ToString("dd-MM-yyyy");
    
    private readonly ObservableCollection<DoctorDto> _doctors;
    private readonly ObservableCollection<DiseaseDto> _diseases;
    private readonly ObservableCollection<ReferralTestDto> _referralTests;

    public ICollectionView DoctorsView { get; set; }
    public ICollectionView DiseasesView { get; set; }
    public ICollectionView ReferralTestsView { get; set; }

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
    
    private ReferralTestDto _selectedReferralTest  = null!;

    [Required(ErrorMessage = "Referral Test is required!")]
    public ReferralTestDto SelectedReferralTest {
        get => _selectedReferralTest;
        set {
            _selectedReferralTest = value;
            Validate(nameof(SelectedReferralTest), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }

    private DiseaseDto _selectedDisease  = null!;
    public DiseaseDto SelectedDisease {
        get => _selectedDisease;
        set {
            _selectedDisease  = value;
            OnPropertyChanged();
        }
    }

    private string _referralName = string.Empty;

    [Required(ErrorMessage = "Referral's Name is required!")]
    [RegularExpression(@"([\p{L}]+[\s]?)+", ErrorMessage = "Use letters only please!")]
    public string ReferralName {
        get => _referralName;
        set {
            _referralName = value;
            Validate(nameof(ReferralName), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }
    
    private string _referralDescription = string.Empty;
    
    [Required(ErrorMessage = "Referral's Description is required!")]
    [RegularExpression(@"([\p{L}]+[\s]?)+", ErrorMessage = "Use letters only please!")]
    public string ReferralDescription { 
        get => _referralDescription;
        set {
            _referralDescription = value;
            Validate(nameof(ReferralDescription), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }
    
    private ICommand LoadFamilyDoctorsCommand { get; set; }
    private ICommand LoadDiseasesCommand { get; }
    private ICommand LoadReferralTestsCommand { get; }
    public ICommand CreateReferralCommand { get; set; }
    public int SelectedPatientId { get; set; }

    private IssueReferralViewModel(PatientStore patientStore, ReferralStore referralStore,  DoctorStore doctorStore, DiseaseStore diseaseStore, ReferralTestStore referralTestStore){
        _doctors = [];
        _diseases = [];
        _referralTests = [];
        SelectedPatientId = patientStore.PatientIdToShowDetails;
        
        DoctorsView = CollectionViewSource.GetDefaultView(_doctors);
        DiseasesView = CollectionViewSource.GetDefaultView(_diseases);
        ReferralTestsView = CollectionViewSource.GetDefaultView(_referralTests);

        LoadFamilyDoctorsCommand = new LoadFamilyDoctorsCommand(UpdateDoctorsDto, doctorStore);
        LoadDiseasesCommand = new LoadDiseasesCommand(UpdateDiseasesDto, diseaseStore);
        LoadReferralTestsCommand = new LoadReferralTestsCommand(UpdateReferralTests, referralTestStore);
        CreateReferralCommand = new CreateReferralCommand(this, referralStore, ResetForm);
    }

    public static IssueReferralViewModel LoadIssueReferralViewModel(PatientStore patientStore, ReferralStore referralStore, DoctorStore doctorStore, DiseaseStore diseaseStore,
        ReferralTestStore referralTestStore){
        var issueReferralViewModel = new IssueReferralViewModel(patientStore, referralStore, doctorStore, diseaseStore, referralTestStore);
        
        issueReferralViewModel.LoadFamilyDoctorsCommand.Execute(null);
        issueReferralViewModel.LoadDiseasesCommand.Execute(null);
        issueReferralViewModel.LoadReferralTestsCommand.Execute(null);
        
        return issueReferralViewModel;
    }
    
    protected override void Submit() {
        CreateReferralCommand.Execute(null);
    }

    public void UpdateDoctorsDto(IEnumerable<DoctorDto> doctorsDto){
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

    public void UpdateReferralTests(IEnumerable<ReferralTestDto> referralTests){
        _referralTests.Clear();

        foreach (var referralTest in referralTests) {
            _referralTests.Add(referralTest);
        }
    }

    protected override void ResetForm(){
        SelectedDoctor = null!;
        SelectedReferralTest = null!;
        SelectedDisease  = null!;
        ReferralDescription = string.Empty;
        ReferralName = string.Empty;
    }
}