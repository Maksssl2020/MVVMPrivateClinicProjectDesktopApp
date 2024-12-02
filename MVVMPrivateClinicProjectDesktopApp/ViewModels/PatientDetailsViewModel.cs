using System.Collections.ObjectModel;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PatientDetailsViewModel : ViewModelBase {
    private readonly PatientStore _patientStore;
    private readonly AppointmentStore _appointmentStore;

    private Patient _selectedPatient = null!;
    private Address _selectedPatientAddress = null!;
    private readonly ICommand LoadPatientDetailsCommand;
    
    public Patient SelectedPatient {
        get => _selectedPatient;
        private set {
            _selectedPatient = value;
            OnPropertyChanged();
        }
    }

    public Address SelectedPatientAddress {
        get => _selectedPatientAddress;
        private set {
            _selectedPatientAddress = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<AppointmentDto> SelectedPatientAppointments { get; }

    private PatientDetailsViewModel(PatientStore patientStore, AppointmentStore appointmentStore) {
        _patientStore = patientStore;
        _appointmentStore = appointmentStore;
        SelectedPatientAppointments = [];
        
        LoadPatientDetailsCommand = new LoadPatientDetailsCommand(this, patientStore, appointmentStore);
    }

    public static PatientDetailsViewModel LoadPatientDetailsViewModel(PatientStore patientStore, AppointmentStore appointmentStore){
        var patientDetailsViewModel = new PatientDetailsViewModel(patientStore, appointmentStore);
        
        patientDetailsViewModel.LoadPatientDetailsCommand.Execute(null);
        
        return patientDetailsViewModel;
    }
    
    public void UpdatePatientDetails(Patient patient, Address address, IEnumerable<AppointmentDto> appointments) {
        SelectedPatient = patient;
        SelectedPatientAddress = address;
        SelectedPatientAppointments.Clear();
        
        foreach (var appointment in appointments) {
            SelectedPatientAppointments.Add(appointment);
        }
    }
}