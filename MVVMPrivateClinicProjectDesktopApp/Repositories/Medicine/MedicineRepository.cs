using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Medicine;

public class MedicineRepository(DbContextFactory dbContextFactory, IMapper mapper) : IMedicineRepository {
    public async Task<MedicineDto> SaveMedicineAsync(SaveMedicineRequest medicineRequest){
        await using var context = dbContextFactory.CreateDbContext();

        var medicine = new Models.Entities.Medicine {
            Name = medicineRequest.Name,
            Description = medicineRequest.Description,
            Type = medicineRequest.Type,
        };
        
        await context.Medicines.AddAsync(medicine);
        await context.SaveChangesAsync();
        
        return mapper.Map<MedicineDto>(medicine);
    }

    public async Task<MedicineDetailsDto> GetMedicineByIdAsync(int medicineId){
        await using var context = dbContextFactory.CreateDbContext();
        
        var medicine = await context.Medicines
            .FirstOrDefaultAsync(m => m.Id == medicineId);
        
        return mapper.Map<MedicineDetailsDto>(medicine);
    }

    public async Task<IEnumerable<MedicineDto>> GetAllMedicinesDtoAsync(){
        await using var context = dbContextFactory.CreateDbContext();
        return await context.Medicines
            .ProjectTo<MedicineDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<MedicineTypeDto>> GetAllExistingMedicineTypesAsync(){
        await using var context = dbContextFactory.CreateDbContext();
        
        return await context.Medicines
            .Select(medicine => medicine.Type)
            .Distinct()
            .Select(medicineType => new MedicineTypeDto {Type = medicineType})
            .ToListAsync();
    }
}