using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Disease;

public interface IDiseaseRepository {
    Task<DiseaseDto> SaveDiseaseAsync(SaveDiseaseRequest diseaseRequest);
    Task<IEnumerable<DiseaseDto>> GetAllDiseasesAsync();
    Task<DiseaseDto?> GetDiseaseByIdAsync(int diseaseId);
}