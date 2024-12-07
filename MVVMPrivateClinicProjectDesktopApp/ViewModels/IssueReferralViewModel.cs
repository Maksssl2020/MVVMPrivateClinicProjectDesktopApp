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

public class IssueReferralViewModel : ViewModelBase, IDiseasesViewModel, IDoctorsViewModel {
    private readonly ObservableCollection<DoctorDto> _doctors;
    private readonly ObservableCollection<DiseaseDto> _diseases;
    private readonly ObservableCollection<ReferralTestDto> _referralTests;

    public ICollectionView DoctorsView { get; set; }
    public ICollectionView DiseasesView { get; set; }
    public ICollectionView ReferralTestsView { get; set; }

    public static string Today => DateTime.Today.ToString("dd-MM-yyyy");


    public DoctorDto SelectedDoctor { get; set; } = null!;
    
    
    private ICommand LoadFamilyDoctorsCommand { get; set; }
    private ICommand LoadDiseasesCommand { get; }
    private ICommand LoadReferralTestsCommand { get; }
    public SubmitCommand SubmitCommand { get; set; }

    private IssueReferralViewModel(DoctorStore doctorStore, DiseaseStore diseaseStore, ReferralTestStore referralTestStore){
        _doctors = [];
        _diseases = [];
        _referralTests = [];

        DoctorsView = CollectionViewSource.GetDefaultView(_doctors);
        DiseasesView = CollectionViewSource.GetDefaultView(_diseases);
        ReferralTestsView = CollectionViewSource.GetDefaultView(_referralTests);

        LoadFamilyDoctorsCommand = new LoadFamilyDoctorsCommand(this, doctorStore);
        LoadDiseasesCommand = new LoadDiseasesCommand(this, diseaseStore);
        LoadReferralTestsCommand = new LoadReferralTestsCommand(this, referralTestStore);
        SubmitCommand = new SubmitCommand(Submit, CanSubmit);
    }

    public static IssueReferralViewModel LoadIssueReferralViewModel(DoctorStore doctorStore, DiseaseStore diseaseStore,
        ReferralTestStore referralTestStore){
        var issueReferralViewModel = new IssueReferralViewModel(doctorStore, diseaseStore, referralTestStore);
        
        issueReferralViewModel.LoadFamilyDoctorsCommand.Execute(null);
        issueReferralViewModel.LoadDiseasesCommand.Execute(null);
        issueReferralViewModel.LoadReferralTestsCommand.Execute(null);
        
        return issueReferralViewModel;
    }
    
    private bool CanSubmit(){
        var context = new ValidationContext(this);
        var results = new List<ValidationResult>();
        return Validator.TryValidateObject(this, context, results, true);
    }

    private void Submit() {
        Console.WriteLine("SUBMIT!");
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
}