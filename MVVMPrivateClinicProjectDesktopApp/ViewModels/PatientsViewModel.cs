using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.Entities;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Address;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PatientsViewModel : ViewModelBase {
    private readonly IPatientRepository _patientRepository;
    
    private string _patientsFilter = string.Empty;
    
    private ObservableCollection<Patient> Patients { get; set; } = [];
    public ICollectionView PatientsView { get; set; }
    
    public string PatientsFilter {
        get => _patientsFilter;
        set {
            _patientsFilter = value;
            OnPropertyChanged();
            PatientsView.Refresh();
        }
    }
    
    public PatientsViewModel(){
        _patientRepository = new PatientRepository();
        
        LoadPatientsAsync();
        
        PatientsView = CollectionViewSource.GetDefaultView(Patients);
        PatientsView.Filter = FilterPatients;
    }

    private async void LoadPatientsAsync(){
        try {
            var patients = await _patientRepository.GetAllPatientsAsync();

            foreach (var patient in patients) {
                Patients.Add(patient);
            }
        }
        catch (Exception e) {
            Console.WriteLine("Something went wrong... " + e.Message);
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