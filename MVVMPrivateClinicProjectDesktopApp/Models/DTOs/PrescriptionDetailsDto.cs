namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class PrescriptionDetailsDto {
    public int Id { get; set; }
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
    public required string PrescriptionDescription { get; set; }
    public DateOnly DateIssued { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public PatientFullNameDto PatientFullNameDto { get; set; } = null!;
    public DoctorFullNameAndSpecializationDto DoctorFullNameAndSpecializationDto { get; set; } = null!;
    public IEnumerable<MedicineDto>? MedicinesDto { get; set; }
}