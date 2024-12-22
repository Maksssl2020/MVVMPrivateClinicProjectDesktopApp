using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Prescription;

public interface IPrescriptionRepository {
    Task<PrescriptionDto> SavePrescriptionAsync(SavePrescriptionRequest prescriptionRequest);
    Task<PrescriptionDetailsDto> GetPrescriptionDetailsDtoByIdAsync(int prescriptionId);
    Task<IEnumerable<PrescriptionDto>> GetAllPrescriptionsDtoAsync();
    Task<IEnumerable<PrescriptionDto>> GetIssuedPrescriptionsByPatientIdOrDoctorId(int personId, PersonType personType);
    Task<int> CountIssuedPrescriptionsAsync();
    Task<int> CountIssuedPrescriptionsByDoctorIdAsync(int doctorId);
    Task<int> CountMedicineUsesAsync(int medicineId);
}