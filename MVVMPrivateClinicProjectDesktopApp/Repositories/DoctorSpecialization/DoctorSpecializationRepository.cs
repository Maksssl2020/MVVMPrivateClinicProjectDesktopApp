using Microsoft.EntityFrameworkCore;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.DoctorSpecialization;

public class DoctorSpecializationRepository : RepositoryBase, IDoctorSpecializationRepository {
    public async Task<IEnumerable<Models.Entities.DoctorSpecialization>> GetAllDoctorSpecializations(){
        return await DbContext.DoctorSpecializations.ToListAsync();
    }

    public async Task<Models.Entities.DoctorSpecialization?> GetDoctorSpecializationById(int id){
        return await DbContext.DoctorSpecializations
            .FirstOrDefaultAsync(doctorSpecialization => doctorSpecialization.Id == id)!;
    }
}