using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Base;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.MedicineRepository;

public class MedicineRepository(DbContextFactory dbContextFactory, IMapper mapper)
    : BaseRepository<Medicine, MedicineDto>(dbContextFactory, mapper), IMedicineRepository {
    private readonly DbContextFactory _dbContextFactory = dbContextFactory;
    private readonly IMapper _mapper = mapper;

    public async Task<MedicineDto> SaveMedicineAsync(SaveMedicineRequest medicineRequest){
        await using var context = _dbContextFactory.CreateDbContext();

        var medicine = new Medicine {
            Name = medicineRequest.Name,
            Description = medicineRequest.Description,
            Type = medicineRequest.Type,
        };
        
        await context.Medicines.AddAsync(medicine);
        await context.SaveChangesAsync();
        
        return _mapper.Map<MedicineDto>(medicine);
    }

    public async Task<MedicineDetailsDto> GetMedicineDetailsByIdAsync(int medicineId){
        await using var context = _dbContextFactory.CreateDbContext();
        
        var medicine = await context.Medicines
            .FirstOrDefaultAsync(m => m.Id == medicineId);
        
        return _mapper.Map<MedicineDetailsDto>(medicine);
    }

    public async Task<IEnumerable<MedicineDto>> GetAllMedicinesDtoAsync(){
        return await GetAllEntitiesAsync();
    }

    public async Task<IEnumerable<MedicineTypeDto>> GetAllExistingMedicineTypesAsync(){
        await using var context = _dbContextFactory.CreateDbContext();
        
        return await context.Medicines
            .Select(medicine => medicine.Type)
            .Distinct()
            .Select(medicineType => new MedicineTypeDto {Type = medicineType})
            .ToListAsync();
    }
}