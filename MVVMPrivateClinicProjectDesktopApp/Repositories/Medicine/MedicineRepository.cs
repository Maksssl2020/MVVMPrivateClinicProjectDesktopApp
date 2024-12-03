using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Medicine;

public class MedicineRepository(DbContextFactory dbContextFactory, IMapper mapper) : IMedicineRepository {
    public async Task<IEnumerable<Models.Entities.Medicine>> GetAllMedicinesAsync(){
        await using var context = dbContextFactory.CreateDbContext();
        return await context.Medicines.ToListAsync();
    }

    public async Task<IEnumerable<MedicineDto>> GetAllMedicinesDtoAsync(){
        await using var context = dbContextFactory.CreateDbContext();
        return await context.Medicines
            .ProjectTo<MedicineDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }
}