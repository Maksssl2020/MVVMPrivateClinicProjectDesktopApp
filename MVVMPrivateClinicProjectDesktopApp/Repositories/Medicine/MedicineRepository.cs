using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Medicine;

public class MedicineRepository(DbContextFactory dbContextFactory) : IMedicineRepository {
    public async Task<IEnumerable<Models.Entities.Medicine>> GetAllMedicinesAsync(){
        await using var context = dbContextFactory.CreateDbContext();
        return await context.Medicines.ToListAsync();
    }
}