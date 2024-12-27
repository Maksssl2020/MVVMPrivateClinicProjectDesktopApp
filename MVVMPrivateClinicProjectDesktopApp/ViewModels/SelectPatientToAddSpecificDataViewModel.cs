using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.IdentityModel.Tokens;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class SelectPatientToAddSpecificDataViewModel : ViewModelBase {
    private readonly PatientStore _patientStore;
    
    public string DataToAddName { get; set; }
    public SolidColorBrush DataColor { get; set; }

    private readonly ObservableCollection<PatientDto> _patients = [];
    public ICollectionView PatientsView { get; set; }

    private PatientDto _selectedPatient = null!;

    [Required(ErrorMessage = "Select a patient!")]
    public PatientDto SelectedPatient {
        get => _selectedPatient;
        set {
            _selectedPatient = value;
            Validate(nameof(SelectedPatient), value);
            SubmitCommand.OnCanExecuteChanged();
        }
    }
    
    private string _patientsFilter = string.Empty;

    public string PatientsFilter {
        get => _patientsFilter;
        set {
            _patientsFilter = value;
            OnPropertyChanged();
            PatientsView.Refresh();
        }
    }

    private ICommand LoadPatientsCommand { get; set; }
    private ICommand ShowPatientDataModalCommand { get; set; }
    public SubmitCommand SubmitCommand { get; set; }

    private SelectPatientToAddSpecificDataViewModel(PatientStore patientStore, AddSpecificDataToPatientStore addSpecificDataToPatientStore, ModalNavigationViewModel modalNavigationViewModel){
        _patientStore = patientStore;
        DataToAddName = addSpecificDataToPatientStore.DataToAddName;
        DataColor = addSpecificDataToPatientStore.DataColor;
        
        PatientsView = CollectionViewSource.GetDefaultView(_patients);
        PatientsView.Filter = FilterPatients;

        LoadPatientsCommand = new LoadEntitiesCommand<PatientDto, PatientDto>(UpdatePatients, patientStore);
        ShowPatientDataModalCommand = modalNavigationViewModel.ShowPatientDataModal;
        SubmitCommand = new SubmitCommand(Submit, CanSubmit);
    }

    private bool CanSubmit(){
        var context = new ValidationContext(this);
        var results = new List<ValidationResult>();
        return Validator.TryValidateObject(this, context, results, true);
    }
    
    private void Submit(){
        SetPatientIdToShowData();
        ShowPatientDataModalCommand.Execute(null);
    }
    
    public static SelectPatientToAddSpecificDataViewModel LoadSelectPatientToAddSpecificDataViewModel(
        PatientStore patientStore, AddSpecificDataToPatientStore addSpecificDataToPatientStore, ModalNavigationViewModel modalNavigationViewModel){
        var selectPatientToAddSpecificDataViewModel = new SelectPatientToAddSpecificDataViewModel(patientStore, addSpecificDataToPatientStore, modalNavigationViewModel);
        
        selectPatientToAddSpecificDataViewModel.LoadPatientsCommand.Execute(null);
        
        return selectPatientToAddSpecificDataViewModel;
    }

    private void SetPatientIdToShowData(){
        _patientStore.EntityIdToShowDetails = SelectedPatient.Id;
    }

    private void UpdatePatients(IEnumerable<PatientDto> patients){
        _patients.Clear();

        foreach (var patient in patients) {
            _patients.Add(patient);
        }
    }

    private bool FilterPatients(object obj){
        if (obj is not PatientDto patientDto) {
            return false;
        }

        if (PatientsFilter.IsNullOrEmpty()) {
            return true;
        }
        
        var filter = PatientsFilter.Trim().ToLower();
        return patientDto.PatientCode.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
               patientDto.FirstName.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
               patientDto.LastName.Contains(filter, StringComparison.CurrentCultureIgnoreCase);
    }
}