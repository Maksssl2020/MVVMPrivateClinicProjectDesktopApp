using System.Collections.ObjectModel;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PatientDetailsViewModel : ViewModelBase {
    private Patient _selectedPatient = null!;
    private Address _selectedPatientAddress = null!;
    public int SelectedPatientId { get; set; }
    
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

    private ICommand LoadPatientDetailsCommand {get; }
    
    private PatientDetailsViewModel(PatientStore patientStore, AppointmentStore appointmentStore) {
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