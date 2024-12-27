using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class DoctorSpecializationStore(IUnitOfWork unitOfWork)
    : EntityStore<DoctorSpecializationDto, DoctorSpecializationDto>(unitOfWork) {
    private DoctorSpecializationDto CreatedDoctorSpecializationDto { get; set; } = null!;
    
    public async Task<int> GetDoctorSpecializationId(string doctorSpecializationName){
        if (await UnitOfWork.DoctorSpecializationRepository.IsDoctorSpecializationExists(doctorSpecializationName)) {
            var foundDoctorSpecializationId = await UnitOfWork.DoctorSpecializationRepository.GetDoctorSpecializationId(doctorSpecializationName);
            
            return foundDoctorSpecializationId ?? -1;
        }

        await CreateEntity(doctorSpecializationName);
        return CreatedDoctorSpecializationDto.Id;
    }

    public override async Task CreateEntity(object entityRequest){
        if (entityRequest is string doctorSpecializationName) {
            var savedDoctorSpecialization = await UnitOfWork.DoctorSpecializationRepository.SaveDoctorSpecializationAsync(
                doctorSpecializationName);
            Entities.Add(savedDoctorSpecialization);
            CreatedDoctorSpecializationDto = savedDoctorSpecialization;
            OnEntityCreated(savedDoctorSpecialization);
        }
    }

    public override async Task DeleteEntity(int entityId){
        await Task.CompletedTask;
    }

    public override async Task LoadEntityDetails(){
        await Task.CompletedTask;
    }

    protected override async Task InitializeEntities(){
        var loadedDoctorSpecializations = await UnitOfWork.DoctorSpecializationRepository.GetAllDoctorSpecializations();
        
        Entities.Clear();
        Entities.AddRange(loadedDoctorSpecializations);
    }
}