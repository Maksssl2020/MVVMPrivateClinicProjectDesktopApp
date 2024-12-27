using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class DiseaseStore(IUnitOfWork unitOfWork) : EntityStore<DiseaseDto, DiseaseDetailsDto>(unitOfWork) {
    public override async Task CreateEntity(object entityRequest){
        if (entityRequest is SaveDiseaseRequest saveDiseaseRequest) {
            var savedDisease = await UnitOfWork.DiseaseRepository.SaveDiseaseAsync(saveDiseaseRequest);
            Entities.Add(savedDisease);
        
            OnEntityCreated(savedDisease);
        }
    }

    public override async Task DeleteEntity(int entityId){
        await UnitOfWork.DiseaseRepository.DeleteEntityAsync(entityId);
        Entities.RemoveAll(e => e.Id == entityId);
        OnEntityDeleted(entityId);
    }

    public override async Task LoadEntityDetails(){
        var foundDisease = await UnitOfWork.DiseaseRepository.GetDiseaseDetailsByIdAsync(EntityIdToShowDetails);
        var countDiagnosedDiseaseAsync = await UnitOfWork.DiagnosesRepository.CountDiagnosedDiseaseAsync(EntityIdToShowDetails);

        if (foundDisease != null) {
            foundDisease.TotalDiagnoses = countDiagnosedDiseaseAsync;
            SelectedEntityDetails = foundDisease;
        }
    }

    protected override async Task InitializeEntities(){
        var loadedDiseases = await UnitOfWork.DiseaseRepository.GetAllDiseasesAsync();
        
        Entities.Clear();
        Entities.AddRange(loadedDiseases);
    }
}