using Microsoft.EntityFrameworkCore;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Disease;

public class DiseaseRepository : RepositoryBase, IDiseaseRepository {
    public async Task<IEnumerable<Models.Entities.Disease>> GetAllDiseasesAsync(){
        return await DbContext.Diseases.ToListAsync();
    }
}