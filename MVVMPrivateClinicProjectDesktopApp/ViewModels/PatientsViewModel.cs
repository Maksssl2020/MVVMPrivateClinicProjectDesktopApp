using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using RelayCommand = CommunityToolkit.Mvvm.Input.RelayCommand;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PatientsViewModel : ViewModelBase {
    private readonly PatientStore _patientStore;

    public ICommand ShowAddNewPatientModal {get; set;}
    private ICommand ShowDeletePatientModal { get; set; }
    public ICommand SetPatientIdToDeleteAndShowWarningModalCommand { get; set; }
    private ICommand LoadPatients { get; set; }
    
    private ObservableCollection<Patient> Patients { get; set; } = [];
    public ICollectionView PatientsView { get; set; }
    
    
    private string _patientsFilter = string.Empty;
    public string PatientsFilter {
        get => _patientsFilter;
        set {
            _patientsFilter = value;
            OnPropertyChanged();
            PatientsView.Refresh();
        }
    }

    private PatientsViewModel(PatientStore patientStore, ModalNavigationViewModel modalNavigationViewModel){
        _patientStore = patientStore;

        LoadPatients = new LoadPatientsCommand(this, patientStore);
        ShowAddNewPatientModal = modalNavigationViewModel.ShowAddNewPatientModal;
        ShowDeletePatientModal = modalNavigationViewModel.ShowDeletePatientModal;
        SetPatientIdToDeleteAndShowWarningModalCommand = new PrepareDeletePatientCommand<int>(SetPatientIdToDeleteAndShowWarningModal);
        
        PatientsView = CollectionViewSource.GetDefaultView(Patients);
        PatientsView.Filter = FilterPatients;
        
        _patientStore.PatientCreated += OnPatientCreated;
        _patientStore.PatientDeleted += OnPatientDeleted;
    }

    public static PatientsViewModel LoadPatientsViewModel(PatientStore patientStore,
        ModalNavigationViewModel modalNavigationViewModel){
        var patientsViewModel = new PatientsViewModel(patientStore, modalNavigationViewModel);
        
        patientsViewModel.LoadPatients.Execute(null);
        
        return patientsViewModel;
    }

    private void SetPatientIdToDeleteAndShowWarningModal(int patientId) {
        Console.WriteLine("CLICK!");
        _patientStore.PatientIdToDelete = patientId;
        ShowDeletePatientModal.Execute(null);
    }
    
    public override void Dispose(){
        _patientStore.PatientCreated -= OnPatientCreated;
        _patientStore.PatientDeleted -= OnPatientDeleted;
        base.Dispose();
    }

    private void OnPatientCreated(Patient patient){
        Patients.Add(patient);
    }

    private void OnPatientDeleted(int patientId){
        var foundPatient = Patients.First(patient => patient.Id == patientId);
        Patients.Remove(foundPatient);
    }

    public void UpdatePatients(IEnumerable<Patient> patients){
        Patients.Clear();

        foreach (var patient in patients) {
            Patients.Add(patient);
        }
    }
    
    private bool FilterPatients(object obj){
        if (obj is not Patient patient) {
            return false;
        }

        if (string.IsNullOrWhiteSpace(PatientsFilter)) {
            return true;
        }
        
        var filter = PatientsFilter.Trim().ToLower();
        return patient.PatientCode!.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
               patient.FirstName.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
               patient.LastName.Contains(filter, StringComparison.CurrentCultureIgnoreCase);
    }
}