using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Prescription;

public interface IPrescriptionRepository {
    Task<IEnumerable<PrescriptionDto>> GetAllPrescriptionsDtoAsync();
}