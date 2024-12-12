using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PatientDetailsViewModel : ViewModelBase {
    private PatientDto _selectedPatient = null!;
    private Address _selectedPatientAddress = null!;
    
    public PatientDto SelectedPatient {
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

    private readonly ObservableCollection<AppointmentDto> _selectedPatientAppointments;
    private readonly ObservableCollection<PrescriptionDto> _selectedPatientPrescriptions;
    private readonly ObservableCollection<ReferralDto> _selectedPatientReferrals;
    private readonly ObservableCollection<DiagnosisDto> _selectedPatientDiagnoses;
    
    public ICollectionView SelectedPatientAppointmentsView { get; set; }
    public ICollectionView SelectedPatientPrescriptionsView { get; set; }
    public ICollectionView SelectedPatientReferralsView { get; set; }
    public ICollectionView SelectedPatientDiagnosesView { get; set; }

    private ICommand LoadPatientDetailsCommand {get; }
    
    private PatientDetailsViewModel(
        PatientStore patientStore,
        AppointmentStore appointmentStore, 
        PrescriptionStore prescriptionStore,
        ReferralStore referralStore,
        DiagnosisStore diagnosisStore
        ) {
        _selectedPatientAppointments = [];
        _selectedPatientPrescriptions = [];
        _selectedPatientReferrals = [];
        _selectedPatientDiagnoses = [];
        
        SelectedPatientAppointmentsView = CollectionViewSource.GetDefaultView(_selectedPatientAppointments);
        SelectedPatientPrescriptionsView = CollectionViewSource.GetDefaultView(_selectedPatientPrescriptions);
        SelectedPatientReferralsView = CollectionViewSource.GetDefaultView(_selectedPatientReferrals);
        SelectedPatientDiagnosesView = CollectionViewSource.GetDefaultView(_selectedPatientDiagnoses);
        
        LoadPatientDetailsCommand = new LoadPatientDetailsCommand(this, patientStore, appointmentStore, prescriptionStore, referralStore, diagnosisStore);

        referralStore.ReferralCreated += OnReferralCreated;
        prescriptionStore.PrescriptionCreated += OnPrescriptionCreated;
    }

    public static PatientDetailsViewModel LoadPatientDetailsViewModel(
        PatientStore patientStore,
        AppointmentStore appointmentStore,
        PrescriptionStore prescriptionStore,
        ReferralStore referralStore,
        DiagnosisStore diagnosisStore
        ){
        var patientDetailsViewModel = new PatientDetailsViewModel(patientStore, appointmentStore, prescriptionStore,referralStore, diagnosisStore);
        
        patientDetailsViewModel.LoadPatientDetailsCommand.Execute(null);
        
        return patientDetailsViewModel;
    }

    private void OnReferralCreated(ReferralDto referralDto){
        _selectedPatientReferrals.Add(referralDto);
    }
    
    private void OnPrescriptionCreated(PrescriptionDto prescription){
        _selectedPatientPrescriptions.Add(prescription);
    }
    
    public void UpdatePatientDetails(PatientDto patient, Address address, IEnumerable<AppointmentDto> appointments, IEnumerable<PrescriptionDto> prescriptions, IEnumerable<ReferralDto> referrals, IEnumerable<DiagnosisDto> diagnoses) {
        SelectedPatient = patient;
        SelectedPatientAddress = address;
        _selectedPatientAppointments.Clear();
        _selectedPatientPrescriptions.Clear();
        _selectedPatientReferrals.Clear();
        _selectedPatientDiagnoses.Clear();
        
        foreach (var appointment in appointments) {
            _selectedPatientAppointments.Add(appointment);
        }

        foreach (var prescription in prescriptions) {
            _selectedPatientPrescriptions.Add(prescription);
        }

        foreach (var referral in referrals) {
            _selectedPatientReferrals.Add(referral);
        }

        foreach (var diagnosis in diagnoses) {
            _selectedPatientDiagnoses.Add(diagnosis);
        }
    }
}