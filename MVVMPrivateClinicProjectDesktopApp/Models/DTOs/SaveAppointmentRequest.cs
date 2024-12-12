namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class SaveAppointmentRequest {
    public DateOnly AppointmentDate { get; set; }
    public TimeOnly AppointmentTime { get; set; }
    public decimal AppointmentCost { get; set; }
    public int IdDoctor { get; set; }
    public int IdPatient { get; set; }
    public int IdPricing { get; set; }
}