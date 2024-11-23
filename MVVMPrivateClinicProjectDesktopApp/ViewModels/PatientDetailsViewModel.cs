using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.Entities;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Address;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PatientDetailsViewModel : ViewModelBase {
    private readonly IPatientRepository _patientRepository;
    private readonly IAddressRepository _addressRepository;
    
    // public ICommand OpenPatientDetailsCommand { get; }

    
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

    public PatientDetailsViewModel(){
        _patientRepository = new PatientRepository();
        _addressRepository = new AddressRepository();
    }

    public PatientDetailsViewModel(int patientId) {
        _patientRepository = new PatientRepository();
        _addressRepository = new AddressRepository();
        
        LoadSelectedPatient(patientId);
    }
    
    private void LoadSelectedPatient(int patientId){
        Console.WriteLine(patientId);
        var foundPatient = _patientRepository.GetPatientById(patientId);
        if (foundPatient == null) return;
        
        SelectedPatient = foundPatient;
        SelectedPatientAddress = LoadSelectedPatientAddress(patientId);
        Console.WriteLine($"Patient Id: {SelectedPatient?.Id}");
        Console.WriteLine($"Address Street: {SelectedPatientAddress?.Street}");
    }

    private Address LoadSelectedPatientAddress(int patientId){
        var foundAddress = _addressRepository.GetAddressByPatientId(patientId)!;
        Console.WriteLine($"Address Street: {foundAddress.Street}");
        return foundAddress;
    }
}