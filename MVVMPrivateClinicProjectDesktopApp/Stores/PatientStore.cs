using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class PatientStore(IUnitOfWork unitOfWork) : EntityStore<PatientDto, PatientDto>(unitOfWork) {
    public Address? SelectedPatientAddress { get; set; }

    public async Task<Address?> SavePatientAddress(SaveAddressRequest patientAddress){
        return await UnitOfWork.AddressRepository.SaveAddressAsync(patientAddress);
    }
    
    public override async Task CreateEntity(object entityRequest){
        if (entityRequest is SavePatientRequest savePatientRequest) {
            var savedPatient = await UnitOfWork.PatientRepository.SavePatientAsync(savePatientRequest);
            Entities.Add(savedPatient);
        
            OnEntityCreated(savedPatient);
        }
    }

    public override async Task DeleteEntity(int entityId){
        await UnitOfWork.PatientRepository.DeleteEntityAsync(entityId);
        Entities.RemoveAll(p => p.Id == entityId);
        OnEntityDeleted(entityId);
    }

    public override async Task LoadEntityDetails(){
        SelectedEntityDetails = (await UnitOfWork.PatientRepository.GetPatientByIdAsync(EntityIdToShowDetails) ?? null) ?? throw new InvalidOperationException();
    }

    protected override async Task InitializeEntities(){
        var loadedPatients = await UnitOfWork.PatientRepository.GetAllPatientsAsync();
        
        Entities.Clear();
        Entities.AddRange(loadedPatients);
    }

    public string? GetSelectedPatientCode(int patientId){
        return Entities.Find(patient => patient.Id == patientId)?.PatientCode;
    }
    
    public async Task LoadPatientDetails(){
        SelectedEntityDetails = (await UnitOfWork.PatientRepository.GetPatientByIdAsync(EntityIdToShowDetails) ?? null) ?? throw new InvalidOperationException();
        SelectedPatientAddress = await UnitOfWork.AddressRepository.GetAddressByPatientId(EntityIdToShowDetails) ?? null;
    }

}