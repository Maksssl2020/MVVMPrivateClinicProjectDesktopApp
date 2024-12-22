using System.Windows.Documents;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class AppointmentStore {
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly List<AppointmentDto> _allAppointments;
    private readonly List<AppointmentDto> _selectedPatientAllAppointments;
    private readonly List<AppointmentDto> _upcomingAppointments;
    private readonly List<AppointmentDto> _selectedDoctorAppointments;
    private readonly Lazy<Task> _initializeLazyAllAppointments;
    private readonly Lazy<Task> _initializeLazyUpcomingAppointments;
    
    public IEnumerable<AppointmentDto> AllAppointments => _allAppointments;
    public IEnumerable<AppointmentDto> SelectedPatientAllAppointments => _selectedPatientAllAppointments;
    public IEnumerable<AppointmentDto> SelectedDoctorAppointments => _selectedDoctorAppointments;
    public IEnumerable<AppointmentDto> UpcomingAppointments => _upcomingAppointments;
    
    public event Action<AppointmentDto>? AppointmentStatusUpdated;
    public event Action<AppointmentDto>? AppointmentCreated;
    
    private int _selectedPatientId;
    public int SelectedPatientId {
        get => _selectedPatientId;
        set {
            _selectedPatientId = value;
            _selectedPatientAllAppointments.Clear();
        }
    }

    private int _selectedDoctorId;
    public int SelectedDoctorId {
        get => _selectedDoctorId;
        set {
            _selectedDoctorId = value;
            _selectedDoctorAppointments.Clear();
        }
    }
    
    public AppointmentStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;
        _allAppointments = [];
        _selectedPatientAllAppointments = [];
        _selectedDoctorAppointments = [];
        _upcomingAppointments = [];
        
        _initializeLazyAllAppointments = new Lazy<Task>(InitializeAppointments);
        _initializeLazyUpcomingAppointments = new Lazy<Task>(InitializeUpcomingAppointments);
    }

    public async Task LoadPatientAppointments(){
        var foundAppointments = await _unitOfWork.AppointmentRepository.GetAppointmentsByPatientIdOrDoctorIdAsync(SelectedPatientId, PersonType.Patient);
        _selectedPatientAllAppointments.AddRange(foundAppointments);
    }

    public async Task LoadDoctorAppointments(){
        var foundAppointments = await _unitOfWork.AppointmentRepository.GetAppointmentsByPatientIdOrDoctorIdAsync(SelectedDoctorId,
            PersonType.Doctor);
        _selectedDoctorAppointments.AddRange(foundAppointments);
    }
    
    public async Task LoadAppointments(){
        await _initializeLazyAllAppointments.Value;
    }

    public async Task LoadUpcomingAppointments(){
        await _initializeLazyUpcomingAppointments.Value;
    }
    
    public async Task UpdateAppointmentStatus(int appointmentId, AppointmentStatus appointmentStatus){
        var appointment = _allAppointments.SingleOrDefault(a => a.Id == appointmentId);
        if (appointment == null) return;
        
        appointment.AppointmentStatus = appointmentStatus.ToString();
        await _unitOfWork.AppointmentRepository.UpdateAppointmentStatusAsync(appointmentId, appointmentStatus);
        
        OnAppointmentStatusUpdated(appointment);
    }

    public async Task CreateAppointment(SaveAppointmentRequest saveAppointmentRequest){
        var savedAppointment = await _unitOfWork.AppointmentRepository.SaveAppointmentAsync(saveAppointmentRequest);
        _allAppointments.Add(savedAppointment);
        
        OnAppointmentCreated(savedAppointment);
    }
    
    private void OnAppointmentStatusUpdated(AppointmentDto appointmentDto){
        AppointmentStatusUpdated?.Invoke(appointmentDto);
    }

    private void OnAppointmentCreated(AppointmentDto appointmentDto){
        AppointmentCreated?.Invoke(appointmentDto);
    }
    
    private async Task InitializeAppointments(){
        var loadedAppointments = await _unitOfWork.AppointmentRepository.GetAllAppointmentsAsync();
        
        _allAppointments.Clear();
        _allAppointments.AddRange(loadedAppointments);
    }

    private async Task InitializeUpcomingAppointments(){
        var loadedUpcomingAppointments = await _unitOfWork.AppointmentRepository.GetUpcomingAppointmentsAsync(2);
        
        _upcomingAppointments.Clear();
        _upcomingAppointments.AddRange(loadedUpcomingAppointments);
    }
}