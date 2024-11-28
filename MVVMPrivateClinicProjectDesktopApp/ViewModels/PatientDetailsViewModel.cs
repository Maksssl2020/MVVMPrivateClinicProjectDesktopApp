using MVVMPrivateClinicProjectDesktopApp.Repositories.Address;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PatientDetailsViewModel : ViewModelBase {
    private readonly IPatientRepository _patientRepository;
    private readonly IAddressRepository _addressRepository;
    
    private Patient _selectedPatient = null!;
    private Address _selectedPatientAddress = null!;
    
    public Patient SelectedPatient {
        get => _selectedPatient;
        set {
            _selectedPatient = value;
            OnPropertyChanged();
        }
    }

    public Address SelectedPatientAddress {
        get => _selectedPatientAddress;
        set {
            _selectedPatientAddress = value;
            OnPropertyChanged();
        }
    }

    public PatientDetailsViewModel() {
        _patientRepository = new PatientRepository();
        _addressRepository = new AddressRepository();
    }

    public PatientDetailsViewModel(int patientId) {
        _patientRepository = new PatientRepository();
        _addressRepository = new AddressRepository();
        
        LoadSelectedPatient(patientId);
    }
    
    private async void LoadSelectedPatient(int patientId){
        try {
            Console.WriteLine(patientId);
            var foundPatient = await _patientRepository.GetPatientById(patientId);
            if (foundPatient == null) return;
        
            SelectedPatient = foundPatient;
            SelectedPatientAddress = LoadSelectedPatientAddress(patientId);
        }
        catch (Exception e) {
            Console.WriteLine("Something went wrong... {0}", e.Message);
        }
    }

    private Address LoadSelectedPatientAddress(int patientId){
        var foundAddress = _addressRepository.GetAddressByPatientId(patientId)!;
        return foundAddress;
    }
}