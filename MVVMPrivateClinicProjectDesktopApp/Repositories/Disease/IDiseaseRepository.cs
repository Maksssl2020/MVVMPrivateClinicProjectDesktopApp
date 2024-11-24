namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Disease;

public interface IDiseaseRepository {
    Task<IEnumerable<Models.Entities.Disease>> GetAllDiseasesAsync();
}