using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Prescription;

public interface IPrescriptionRepository {
    Task<PrescriptionDto> SavePrescriptionAsync(SavePrescriptionRequest prescriptionRequest);
    Task<PrescriptionDetailsDto> GetPrescriptionDetailsDtoByIdAsync(int prescriptionId);
    Task<IEnumerable<PrescriptionDto>> GetAllPrescriptionsDtoAsync();
    Task<IEnumerable<PrescriptionDto>> GetPatientAllPrescriptionsDtoAsync(int patientId);
    Task<int> CountIssuedPrescriptionsAsync();
}