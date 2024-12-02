using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class PatientStore {
    private readonly IUnitOfWork _unitOfWork;
    
    public event Action<Patient>? PatientCreated;
    public event Action<int>? PatientDeleted;
    
    private readonly List<Patient> _patients;
    private readonly Lazy<Task> _initializeLazy;
    
    public int PatientIdToDelete { get; set; }
    public int PatientIdToShowDetails { get; set; }
    
    public IEnumerable<Patient> Patients => _patients;
    
    public Patient? SelectedPatientData { get; set; }
    public Address? SelectedPatientAddress { get; set; }

    public PatientStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;
        _patients = [];
        _initializeLazy = new Lazy<Task>(InitializePatients);
    }
    
    public async Task<Address?> SavePatientAddress(SaveAddressRequest patientAddress){
        return await _unitOfWork.AddressRepository.SaveAddressAsync(patientAddress);
    }
    
    public async Task CreatePatient(SavePatientRequest patient){
        var savedPatient = await _unitOfWork.PatientRepository.SavePatientAsync(patient);
        _patients.Add(savedPatient);
        
        OnPatientCreated(savedPatient);
    }
    
    private void OnPatientCreated(Patient patient){
        PatientCreated?.Invoke(patient);
    }
    
    public void DeletePatient(int patientId){
        _unitOfWork.PatientRepository.DeletePatient(patientId);
        _patients.RemoveAll(p => p.Id == patientId);
        
        OnPatientDeleted(patientId);
    }

    public string? GetSelectedPatientCode(int patientId){
        return _patients.Find(patient => patient.Id == patientId)?.PatientCode;
    }
    
    private void OnPatientDeleted(int patientId){
        PatientDeleted?.Invoke(patientId);
    }

    public async Task LoadPatientData(){
        SelectedPatientData = await _unitOfWork.PatientRepository.GetPatientByIdAsync(PatientIdToShowDetails) ?? null;
    }
    
    public async Task LoadPatientDetails(){
        SelectedPatientData = await _unitOfWork.PatientRepository.GetPatientByIdAsync(PatientIdToShowDetails) ?? null;
        SelectedPatientAddress = await _unitOfWork.AddressRepository.GetAddressByPatientId(PatientIdToShowDetails) ?? null;
    }
    
    public async Task LoadPatients(){
        await _initializeLazy.Value;
    }
    
    private async Task InitializePatients(){
        var loadedPatients = await _unitOfWork.PatientRepository.GetAllPatientsAsync();
        
        _patients.Clear();
        _patients.AddRange(loadedPatients);
    }
}