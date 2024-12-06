namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Disease;

public interface IDiseaseRepository {
    Task<IEnumerable<Models.Entities.Disease>> GetAllDiseasesAsync();
    Task<Models.Entities.Disease?> GetDiseaseByIdAsync(int diseaseId);
}