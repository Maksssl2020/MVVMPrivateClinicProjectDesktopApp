namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class DoctorStatisticsDto {
    public int AmountOfAppointments { get; set; }
    public int IssuedReferrals { get; set; }
    public int IssuedPrescriptions { get; set; }
    public int IssuedDiagnosis { get; set; }
    public int IssuedPatientNotes { get; set; }
}