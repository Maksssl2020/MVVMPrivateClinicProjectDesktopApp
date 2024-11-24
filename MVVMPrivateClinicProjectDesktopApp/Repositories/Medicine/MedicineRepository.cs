using Microsoft.EntityFrameworkCore;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Medicine;

public class MedicineRepository : RepositoryBase, IMedicineRepository {
    public async Task<IEnumerable<Models.Entities.Medicine>> GetAllMedicinesAsync(){
        return await DbContext.Medicines.ToListAsync();
    }
}