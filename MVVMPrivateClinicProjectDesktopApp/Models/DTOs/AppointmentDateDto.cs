namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class AppointmentDateDto {
    public DateOnly AppointmentDate { get; set; }
    public TimeOnly AppointmentTime { get; set; }
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
}