using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Disease;

public interface IDiseaseRepository {
    Task<DiseaseDto> SaveDiseaseAsync(SaveDiseaseRequest diseaseRequest);
    Task<DiseaseDto?> GetDiseaseByIdAsync(int diseaseId);
    Task<DiseaseDetailsDto?> GetDiseaseDetailsByIdAsync(int diseaseId);
    Task<IEnumerable<DiseaseDto>> GetAllDiseasesAsync();
}