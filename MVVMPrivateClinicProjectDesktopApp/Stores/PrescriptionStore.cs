using System.Windows.Documents;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class PrescriptionStore(IUnitOfWork unitOfWork)
    : EntityStore<PrescriptionDto, PrescriptionDetailsDto>(unitOfWork) {
    private readonly List<PrescriptionDto> _selectedPatientPrescriptionsDto = [];
    private readonly List<PrescriptionDto> _selectedDoctorIssuedPrescriptionsDto = [];

    public IEnumerable<PrescriptionDto> SelectedPatientPrescriptionsDto => _selectedPatientPrescriptionsDto;
    public IEnumerable<PrescriptionDto> SelectedDoctorIssuedPrescriptions => _selectedDoctorIssuedPrescriptionsDto;
    
    private int _selectedPatientId;
    public int SelectedPatientId {
        get => _selectedPatientId;
        set {
            _selectedPatientId = value;
            _selectedPatientPrescriptionsDto.Clear();
        }
    }
    
    private int _selectedDoctorId;

    public int SelectedDoctorId {
        get => _selectedDoctorId;
        set {
            _selectedDoctorId = value;
            _selectedDoctorIssuedPrescriptionsDto.Clear();
        }
    }
    
    public async Task LoadPatientPrescriptions(){
        var loadedPatientPrescriptions = await UnitOfWork.PrescriptionRepository.GetIssuedPrescriptionsByPatientIdOrDoctorId(SelectedPatientId, PersonType.Patient);
        _selectedPatientPrescriptionsDto.AddRange(loadedPatientPrescriptions);
    }

    public async Task LoadDoctorIssuedPrescriptions(){
        var foundPrescriptions = await UnitOfWork.PrescriptionRepository.GetIssuedPrescriptionsByPatientIdOrDoctorId(SelectedDoctorId,
            PersonType.Doctor);
        _selectedDoctorIssuedPrescriptionsDto.AddRange(foundPrescriptions);
    }
    
    public override async Task CreateEntity(object entityRequest){
        if (entityRequest is SavePrescriptionRequest savePrescriptionRequest) {
            var savedPrescription = await UnitOfWork.PrescriptionRepository.SavePrescriptionAsync(savePrescriptionRequest);
            Entities.Add(savedPrescription);
            _selectedPatientPrescriptionsDto.Add(savedPrescription);
            
            OnEntityCreated(savedPrescription);
        }
    }

    public override async Task DeleteEntity(int entityId){
        await UnitOfWork.PrescriptionRepository.DeleteEntityAsync(entityId);
        Entities.RemoveAll(e => e.Id == entityId);
        OnEntityDeleted(entityId);
    }

    public override async Task LoadEntityDetails(){
        var loadedPrescription = await UnitOfWork.PrescriptionRepository.GetPrescriptionDetailsDtoByIdAsync(EntityIdToShowDetails);
        SelectedEntityDetails = loadedPrescription;
    }

    public async Task GeneratePrescriptionPdfDocument(int prescriptionId){
        var loadedPrescription = await UnitOfWork.PrescriptionRepository.GetPrescriptionDetailsDtoByIdAsync(prescriptionId);
        PdfGenerator.GeneratePrescriptionPdf(loadedPrescription);
    }
    
    protected override async Task InitializeEntities(){
        var prescriptionsDto = await UnitOfWork.PrescriptionRepository.GetAllPrescriptionsDtoAsync();
        
        Entities.Clear();
        Entities.AddRange(prescriptionsDto);
    }
}