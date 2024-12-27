using System.Windows.Documents;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class ReferralTestStore(IUnitOfWork unitOfWork)
    : EntityStore<ReferralTestDto, ReferralTestDetailsDto>(unitOfWork) {
    
    public override async Task CreateEntity(object entityRequest){
        if (entityRequest is SaveReferralTestRequest saveReferralTestRequest) {
            var savedReferralTest = await UnitOfWork.ReferralTestRepository.SaveReferralTestAsync(saveReferralTestRequest);
            Entities.Add(savedReferralTest);
            OnEntityCreated(savedReferralTest);
        }
    }

    public override async Task DeleteEntity(int entityId){
        await UnitOfWork.ReferralTestRepository.DeleteEntityAsync(entityId);
        Entities.RemoveAll(x => x.Id == entityId);
        OnEntityDeleted(entityId);
    }

    public override async Task LoadEntityDetails(){
        var foundReferral = await UnitOfWork.ReferralTestRepository.GetReferralTestDetailsByIdAsync(EntityIdToShowDetails);

        if (foundReferral != null) {
            var referralTestUses = await UnitOfWork.ReferralRepository.CountReferralTestUsesAsync(EntityIdToShowDetails);
            
            foundReferral.TotalUses = referralTestUses;
            SelectedEntityDetails = foundReferral;
        }
    }

    protected override async Task InitializeEntities(){
        var loadedReferralTests = await UnitOfWork.ReferralTestRepository.GetAllReferralTestsDtoAsync();
        
        Entities.Clear();
        Entities.AddRange(loadedReferralTests);
    }
}