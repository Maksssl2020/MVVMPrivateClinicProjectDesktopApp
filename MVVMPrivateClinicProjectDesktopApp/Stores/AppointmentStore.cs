using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Appointment;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class AppointmentStore {
    private readonly IAppointmentRepository _appointmentRepository = new AppointmentRepository(MyMapper.Mapper);

    private readonly List<AppointmentDto> _appointments;
    private readonly Lazy<Task> _initializeLazy;
    public IEnumerable<AppointmentDto> Appointments => _appointments;

    public AppointmentStore(){
        _appointments = [];
        _initializeLazy = new Lazy<Task>(InitializeAppointments);
    }

    public async Task LoadAppointments(){
        await _initializeLazy.Value;
    }

    private async Task InitializeAppointments(){
        var loadedAppointments = await _appointmentRepository.GetAllAppointmentsAsync();
        
        _appointments.Clear();
        _appointments.AddRange(loadedAppointments);
    }
}