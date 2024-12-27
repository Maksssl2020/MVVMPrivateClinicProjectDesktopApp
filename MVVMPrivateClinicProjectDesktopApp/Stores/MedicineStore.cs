using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class MedicineStore : EntityStore<MedicineDto, MedicineDetailsDto> {
    private readonly List<MedicineTypeDto> _medicineTypes;
    private readonly Lazy<Task> _initializeMedicineTypesLazy;
    
    public IEnumerable<MedicineTypeDto> MedicineTypes => _medicineTypes;
    
    public MedicineStore(IUnitOfWork unitOfWork)
        :base(unitOfWork) {
        _medicineTypes = [];
        _initializeMedicineTypesLazy = new Lazy<Task>(InitializeMedicineTypes);
    }

    public async Task LoadMedicineTypes(){
        await _initializeMedicineTypesLazy.Value;
    }
    
    private async Task InitializeMedicineTypes(){
        var loadedMedicineTypes = await UnitOfWork.MedicineRepository.GetAllExistingMedicineTypesAsync();
        
        _medicineTypes.Clear();
        _medicineTypes.AddRange(loadedMedicineTypes);
    }

    public override async Task CreateEntity(object entityRequest){
        if (entityRequest is SaveMedicineRequest saveMedicineRequest) {
            var savedMedicine = await UnitOfWork.MedicineRepository.SaveMedicineAsync(saveMedicineRequest);
            Entities.Add(savedMedicine);
        

            var exists = _medicineTypes.Exists(medicineTypeDto => medicineTypeDto.Type == savedMedicine.Type);
            if (!exists) {
                _medicineTypes.Add(new MedicineTypeDto { Type = savedMedicine.Type });
            }
        
            OnEntityCreated(savedMedicine);
        }
    }

    public override async Task DeleteEntity(int entityId){
        await UnitOfWork.MedicineRepository.DeleteEntityAsync(entityId);
        Entities.RemoveAll(m => m.Id == entityId);
        OnEntityDeleted(entityId);
    }

    public override async Task LoadEntityDetails(){
        var foundMedicine = await UnitOfWork.MedicineRepository.GetMedicineDetailsByIdAsync(EntityIdToShowDetails);
        var medicineUses = await UnitOfWork.PrescriptionRepository.CountMedicineUsesAsync(EntityIdToShowDetails);
        foundMedicine.TotalUses = medicineUses;
        SelectedEntityDetails = foundMedicine;
    }

    protected override async Task InitializeEntities(){
        var loadedMedicinesDto = await UnitOfWork.MedicineRepository.GetAllMedicinesDtoAsync();
        
        Entities.Clear();
        Entities.AddRange(loadedMedicinesDto);
    }
}