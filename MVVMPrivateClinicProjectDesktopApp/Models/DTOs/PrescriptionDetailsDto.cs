namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class PrescriptionDetailsDto {
    public int Id { get; set; }
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
    public required string PrescriptionDescription { get; set; }
    public DateOnly DateIssued { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public PatientDetailsDto PatientDetailsCto { get; set; } = null!;
    public DoctorDetailsDto DoctorDetailsDto { get; set; } = null!;
    public IEnumerable<MedicineDto>? MedicinesDto { get; set; }
}