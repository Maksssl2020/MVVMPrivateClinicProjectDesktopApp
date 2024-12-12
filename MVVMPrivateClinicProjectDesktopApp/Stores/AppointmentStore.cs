using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class AppointmentStore {
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly List<AppointmentDto> _allAppointments;
    private readonly Lazy<Task> _initializeLazy;
    private readonly List<AppointmentDto> _selectedPatientAllAppointments;
    private int _selectedPatientId;
    
    public IEnumerable<AppointmentDto> AllAppointments => _allAppointments;
    public IEnumerable<AppointmentDto> SelectedPatientAllAppointments => _selectedPatientAllAppointments;
    
    public event Action<AppointmentDto>? AppointmentStatusUpdated;
    public event Action<AppointmentDto>? AppointmentCreated;
    
    public int SelectedPatientId {
        get => _selectedPatientId;
        set {
            _selectedPatientId = value;
            _selectedPatientAllAppointments.Clear();
        }
    }
    
    public AppointmentStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;
        _allAppointments = [];
        _selectedPatientAllAppointments = [];
        _initializeLazy = new Lazy<Task>(InitializeAppointments);
    }

    public async Task LoadPatientAppointments(){
        var foundAppointments = await _unitOfWork.AppointmentRepository.GetAppointmentsByPatientIdAsync(SelectedPatientId);
        _selectedPatientAllAppointments.AddRange(foundAppointments);
    }
    
    public async Task LoadAppointments(){
        await _initializeLazy.Value;
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
}