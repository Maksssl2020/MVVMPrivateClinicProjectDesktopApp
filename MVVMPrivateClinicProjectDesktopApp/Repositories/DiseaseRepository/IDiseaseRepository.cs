using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Base;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.DiseaseRepository;

public interface IDiseaseRepository : IBaseRepository<DiseaseDto> {
    Task<DiseaseDto> SaveDiseaseAsync(SaveDiseaseRequest diseaseRequest);
    Task<DiseaseDto?> GetDiseaseByIdAsync(int diseaseId);
    Task<DiseaseDetailsDto?> GetDiseaseDetailsByIdAsync(int diseaseId);
    Task<IEnumerable<DiseaseDto>> GetAllDiseasesAsync();
}