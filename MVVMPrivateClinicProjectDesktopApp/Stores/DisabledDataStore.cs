using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.DisabledDataRepository;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class DisabledDataStore(IUnitOfWork unitOfWork) : EntityStore<DisabledDataDto, DisabledDataDto>(unitOfWork) {
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public string Category { get; set; } = string.Empty;

    public async Task ReloadData(){
        await InitializeEntities();
    }
    
    public override async Task CreateEntity(object entityRequest){
        await Task.CompletedTask;
    }

    public override async Task DeleteEntity(int entityId){
        await RestoreDisabledDataDependsOnCategory(entityId);
        var entityToRestore = Entities.FirstOrDefault(e => e.Id == entityId);
        
        if (entityToRestore != null) {
            Entities.Remove(entityToRestore);
            OnEntityDeleted(entityId);
        }
    }

    public override async Task LoadEntityDetails(){
        await Task.CompletedTask;
    }
    
    protected override async Task InitializeEntities(){
        Entities.Clear();

        var disabledData = await GetDisabledDataDependsOnCategory(Category);
        
        foreach (var disabledDataDto in disabledData) {
            Entities.Add(disabledDataDto);
        }
    }
    
    private async Task<IEnumerable<DisabledDataDto>> GetDisabledDataDependsOnCategory(string category) =>
        category switch {
            "Patients" => await GetRepositoryDependsOnCategory<Patient>(),
            "Doctors" => await GetRepositoryDependsOnCategory<Doctor>(),
            "Diagnoses" => await GetRepositoryDependsOnCategory<Diagnosis>(),
            "Diseases" => await GetRepositoryDependsOnCategory<Disease>(),
            "Pricing" => await GetRepositoryDependsOnCategory<Pricing>(),
            "Pat. Notes" => await GetRepositoryDependsOnCategory<PatientNote>(),
            "Prescriptions" => await GetRepositoryDependsOnCategory<Prescription>(),
            "Medicines" => await GetRepositoryDependsOnCategory<Medicine>(),
            "Invoices" => await GetRepositoryDependsOnCategory<Invoice>(),
            "Ref. Tests" => await GetRepositoryDependsOnCategory<ReferralTest>(),
            "Referrals" => await GetRepositoryDependsOnCategory<Referral>(),
            _ => throw new Exception($"Invalid category: {category}")
        };

    private async Task RestoreDisabledDataDependsOnCategory(int dataId) {
        switch (Category) {
            case "Patients":
                await RestoreRepositoryDependsOnCategory<Patient>(dataId);
                break;
            case "Doctors":
                await RestoreRepositoryDependsOnCategory<Doctor>(dataId);
                break;
            case "Diagnoses":
                await RestoreRepositoryDependsOnCategory<Diagnosis>(dataId);
                break;
            case "Diseases":
                await RestoreRepositoryDependsOnCategory<Disease>(dataId);
                break;
            case "Pricing":
                await RestoreRepositoryDependsOnCategory<Pricing>(dataId);
                break;
            case "Pat. Notes":
                await RestoreRepositoryDependsOnCategory<PatientNote>(dataId);
                break;
            case "Prescriptions":
                await RestoreRepositoryDependsOnCategory<Prescription>(dataId);
                break;
            case "Medicines":
                await RestoreRepositoryDependsOnCategory<Medicine>(dataId);
                break;
            case "Invoices":
                await RestoreRepositoryDependsOnCategory<Invoice>(dataId);
                break;
            case "Ref. Tests":
                await RestoreRepositoryDependsOnCategory<ReferralTest>(dataId);
                break;
            case "Referrals":
                await RestoreRepositoryDependsOnCategory<Referral>(dataId);
                break;
            default:
                throw new Exception($"Invalid category: {Category}");
        }
    }
    
    private async Task<IEnumerable<DisabledDataDto>> GetRepositoryDependsOnCategory<T>() where T : class, IDeletableEntity {
        return await new DisabledDataRepository<T>(_unitOfWork.DbContextFactory).GetDisabledDataAsync();
    }
    
    private async Task RestoreRepositoryDependsOnCategory<T>(int dataId) where T : class, IDeletableEntity {
        await new DisabledDataRepository<T>(_unitOfWork.DbContextFactory).RestoreDataAsync(dataId);
    }
}