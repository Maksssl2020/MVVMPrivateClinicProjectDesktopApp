using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class PatientStore {
    private readonly IPatientRepository _patientRepository = new PatientRepository();
    
    public event Action<Patient>? PatientCreated;
    public event Action<int>? PatientDeleted;
    
    private readonly List<Patient> _patients;
    private readonly Lazy<Task> _initializeLazy;
    public int PatientIdToDelete { get; set; }
    public IEnumerable<Patient> Patients => _patients;

    public PatientStore(){
        _patients = [];
        _initializeLazy = new Lazy<Task>(InitializePatients);
    }
    
    public async Task CreatePatient(SavePatientRequest patient){
        var savedPatient = await _patientRepository.SavePatientAsync(patient);
        _patients.Add(savedPatient);
        
        OnPatientCreated(savedPatient);
    }
    
    private void OnPatientCreated(Patient patient){
        PatientCreated?.Invoke(patient);
    }
    
    public void DeletePatient(int patientId){
        _patientRepository.DeletePatient(patientId);
        _patients.RemoveAll(p => p.Id == patientId);
        
        OnPatientDeleted(patientId);
    }

    private void OnPatientDeleted(int patientId){
        PatientDeleted?.Invoke(patientId);
    }
    
    public async Task LoadPatients(){
        await _initializeLazy.Value;
    }
    
    private async Task InitializePatients(){
        var loadedPatients = await _patientRepository.GetAllPatientsAsync();
        
        _patients.Clear();
        _patients.AddRange(loadedPatients);
    }
}