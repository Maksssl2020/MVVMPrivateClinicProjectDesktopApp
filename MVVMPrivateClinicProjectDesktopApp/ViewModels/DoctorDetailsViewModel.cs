using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class DoctorDetailsViewModel : ViewModelBase {
    private readonly int _selectedDoctorId;
    public int SelectedDoctorId {
        get => _selectedDoctorId;
        private init {
            _selectedDoctorId = value;
            OnPropertyChanged();
        }
    }
    
    private DoctorStatisticsDto _doctorStatistics = null!;
    public DoctorStatisticsDto DoctorStatistics {
        get => _doctorStatistics;
        set {
            _doctorStatistics = value;
            OnPropertyChanged();
        }
    }

    private readonly ObservableCollection<AppointmentDto> _doctorAppointments = [];
    private readonly ObservableCollection<PatientNoteWithDoctorDataDto> _issuedPatientNotes = [];
    private readonly ObservableCollection<PrescriptionDto> _issuedPrescriptions = [];
    private readonly ObservableCollection<ReferralDto> _issuedReferrals = [];
    private readonly ObservableCollection<DiagnosisDto> _issuedDiagnoses = [];
    
    public ICollectionView DoctorAppointmentsView { get; set; }
    public ICollectionView IssuedPatientNotesView { get; set; }
    public ICollectionView IssuedPrescriptionsView { get; set; }
    public ICollectionView IssuedReferralsView { get; set; }
    public ICollectionView IssuedDiagnosesView { get; set; }
    
    private ICommand LoadDoctorStatistics { get; }
    private ICommand LoadDoctorAppointments { get; }
    private ICommand LoadIssuedPatientNotes { get; }
    private ICommand LoadIssuedPrescriptions { get; }
    private ICommand LoadIssuedReferrals { get; }
    private ICommand LoadIssuedDiagnoses { get; }

    private DoctorDetailsViewModel(DoctorStore doctorStore, AppointmentStore appointmentStore, PatientNoteStore patientNoteStore, PrescriptionStore prescriptionStore, ReferralStore referralStore, DiagnosisStore diagnosisStore){
        SelectedDoctorId = doctorStore.SelectedDoctorId;

        DoctorAppointmentsView = CollectionViewSource.GetDefaultView(_doctorAppointments);
        IssuedPatientNotesView = CollectionViewSource.GetDefaultView(_issuedPatientNotes);
        IssuedPrescriptionsView = CollectionViewSource.GetDefaultView(_issuedPrescriptions);
        IssuedReferralsView = CollectionViewSource.GetDefaultView(_issuedReferrals);
        IssuedDiagnosesView = CollectionViewSource.GetDefaultView(_issuedDiagnoses);
        
        LoadDoctorStatistics = new LoadDoctorStatisticsCommand(this, doctorStore);
        LoadDoctorAppointments = new LoadDoctorAppointments(this, appointmentStore);
        LoadIssuedPatientNotes = new LoadDoctorIssuedPatientNotes(this, patientNoteStore);
        LoadIssuedPrescriptions = new LoadDoctorIssuedPrescriptions(this, prescriptionStore);
        LoadIssuedReferrals = new LoadDoctorIssuedReferrals(this, referralStore);
        LoadIssuedDiagnoses = new LoadDoctorIssuedDiagnosesCommand(this, diagnosisStore);
    }

    public static DoctorDetailsViewModel LoadDoctorDetailsViewModel(DoctorStore doctorStore, AppointmentStore appointmentStore, PatientNoteStore patientNoteStore, PrescriptionStore prescriptionStore, ReferralStore referralStore, DiagnosisStore diagnosisStore){
        var doctorDetailsViewModel = new DoctorDetailsViewModel(doctorStore, appointmentStore, patientNoteStore, prescriptionStore, referralStore, diagnosisStore);
        
        doctorDetailsViewModel.LoadDoctorStatistics.Execute(null);
        doctorDetailsViewModel.LoadDoctorAppointments.Execute(null);
        doctorDetailsViewModel.LoadIssuedPatientNotes.Execute(null);
        doctorDetailsViewModel.LoadIssuedPrescriptions.Execute(null);
        doctorDetailsViewModel.LoadIssuedReferrals.Execute(null);
        doctorDetailsViewModel.LoadIssuedDiagnoses.Execute(null);
        
        return doctorDetailsViewModel;
    }

    public void UpdateAppointments(IEnumerable<AppointmentDto> selectedDoctorAppointments){
        _doctorAppointments.Clear();

        foreach (var doctorAppointment in selectedDoctorAppointments) {
            _doctorAppointments.Add(doctorAppointment);
        }
    }

    public void UpdateIssuedPatientNotes(IEnumerable<PatientNoteWithDoctorDataDto> issuedPatientNotes){
        _issuedPatientNotes.Clear();

        foreach (var issuedPatientNote in issuedPatientNotes) {
            _issuedPatientNotes.Add(issuedPatientNote);
        }
    }

    public void UpdateIssuedPrescriptions(IEnumerable<PrescriptionDto> issuedPrescriptions){
        _issuedPrescriptions.Clear();

        foreach (var issuedPrescription in issuedPrescriptions) {
            _issuedPrescriptions.Add(issuedPrescription);
        }
    }

    public void UpdateIssuedReferrals(IEnumerable<ReferralDto> issuedReferrals){
        _issuedReferrals.Clear();

        foreach (var issuedReferral in issuedReferrals) {
            _issuedReferrals.Add(issuedReferral);
        }
    }

    public void UpdateIssuedDiagnoses(IEnumerable<DiagnosisDto> issuedDiagnoses){
        _issuedDiagnoses.Clear();

        foreach (var issuedDiagnosis in issuedDiagnoses) {
            _issuedDiagnoses.Add(issuedDiagnosis);
        }
    }
}