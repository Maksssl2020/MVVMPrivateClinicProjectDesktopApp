using System.Windows.Documents;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class AppointmentStore : EntityStore<AppointmentDto, AppointmentDto> {
    private readonly List<AppointmentDto> _selectedPatientAppointments;
    private readonly List<AppointmentDto> _upcomingAppointments;
    private readonly List<AppointmentDto> _selectedDoctorAppointments;
    private readonly Lazy<Task> _initializeLazyUpcomingAppointments;
    
    public IEnumerable<AppointmentDto> SelectedPatientAppointments => _selectedPatientAppointments;
    public IEnumerable<AppointmentDto> SelectedDoctorAppointments => _selectedDoctorAppointments;
    public IEnumerable<AppointmentDto> UpcomingAppointments => _upcomingAppointments;
    
    private int _selectedPatientId;
    public int SelectedPatientId {
        get => _selectedPatientId;
        set {
            _selectedPatientId = value;
            _selectedPatientAppointments.Clear();
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
    
    public event Action<AppointmentDto>? AppointmentStatusUpdated;
    
    public AppointmentStore(IUnitOfWork unitOfWork)
        :base(unitOfWork){
        _selectedPatientAppointments = [];
        _selectedDoctorAppointments = [];
        _upcomingAppointments = [];
        
        _initializeLazyUpcomingAppointments = new Lazy<Task>(InitializeUpcomingAppointments);
    }

    public async Task LoadPatientAppointments(){
        var foundAppointments = await UnitOfWork.AppointmentRepository.GetAppointmentsByPatientIdOrDoctorIdAsync(SelectedPatientId, PersonType.Patient);
        _selectedPatientAppointments.AddRange(foundAppointments);
    }

    public async Task LoadDoctorAppointments(){
        var foundAppointments = await UnitOfWork.AppointmentRepository.GetAppointmentsByPatientIdOrDoctorIdAsync(SelectedDoctorId,
            PersonType.Doctor);
        _selectedDoctorAppointments.AddRange(foundAppointments);
    }

    public async Task LoadUpcomingAppointments(){
        await _initializeLazyUpcomingAppointments.Value;
    }
    
    public async Task UpdateAppointmentStatus(int appointmentId, AppointmentStatus appointmentStatus){
        var appointment = Entities.SingleOrDefault(a => a.Id == appointmentId);
        if (appointment == null) return;
        
        appointment.AppointmentStatus = appointmentStatus.ToString();
        await UnitOfWork.AppointmentRepository.UpdateAppointmentStatusAsync(appointmentId, appointmentStatus);
        
        OnAppointmentStatusUpdated(appointment);
    }
    
    private void OnAppointmentStatusUpdated(AppointmentDto appointmentDto){
        AppointmentStatusUpdated?.Invoke(appointmentDto);
    }
    
    private async Task InitializeUpcomingAppointments(){
        var loadedUpcomingAppointments = await UnitOfWork.AppointmentRepository.GetUpcomingAppointmentsAsync(2);
        
        _upcomingAppointments.Clear();
        _upcomingAppointments.AddRange(loadedUpcomingAppointments);
    }

    public override async Task CreateEntity(object entityRequest){
        if (entityRequest is SaveAppointmentRequest saveAppointmentRequest) {
            var savedAppointment = await UnitOfWork.AppointmentRepository.SaveAppointmentAsync(saveAppointmentRequest);
            Entities.Add(savedAppointment);
        
            OnEntityCreated(savedAppointment);
        }
    }

    public override async Task DeleteEntity(int entityId){
        await Task.CompletedTask;
    }

    public override async Task LoadEntityDetails(){
        var foundAppointment = await UnitOfWork.AppointmentRepository.GetAppointmentByIdAsync(EntityIdToShowDetails);

        if (foundAppointment != null) {
            SelectedEntityDetails = foundAppointment;
        }
    }

    protected override async Task InitializeEntities(){
        var loadedAppointments = await UnitOfWork.AppointmentRepository.GetAllAppointmentsAsync();
        
        Entities.Clear();
        Entities.AddRange(loadedAppointments);
    }
}