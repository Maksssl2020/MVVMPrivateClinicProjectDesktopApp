using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class DiagnosisStore(IUnitOfWork unitOfWork) : EntityStore<DiagnosisDto, DiagnosisDto>(unitOfWork) {
    private readonly List<DiagnosisDto> _selectedPatientDiagnoses = [];
    private readonly List<DiagnosisDto> _doctorIssuedDiagnoses = [];
    public IEnumerable<DiagnosisDto> SelectedPatientDiagnoses => _selectedPatientDiagnoses;
    public IEnumerable<DiagnosisDto> DoctorIssuedDiagnoses => _doctorIssuedDiagnoses;
    
    private int _selectedPatientId;
    public int SelectedPatientId {
        get => _selectedPatientId;
        set {
            _selectedPatientId = value;
            _selectedPatientDiagnoses.Clear();
        }
    }
    
    private int _selectedDoctorId;
    public int SelectedDoctorId {
        get => _selectedDoctorId;
        set {
            _selectedDoctorId = value;
            _selectedPatientDiagnoses.Clear();
        }
    }

    public async Task LoadPatientDiagnoses(){
        var loadedPatientDiagnoses = await UnitOfWork.DiagnosesRepository.GetIssuedDiagnosesByPatientIdOrDoctorId(SelectedPatientId, PersonType.Patient);
        _selectedPatientDiagnoses.AddRange(loadedPatientDiagnoses);
    }

    public async Task LoadDoctorIssuedDiagnoses(){
        var loadedDoctorDiagnoses = await UnitOfWork.DiagnosesRepository.GetIssuedDiagnosesByPatientIdOrDoctorId(SelectedPatientId, PersonType.Doctor);
        _doctorIssuedDiagnoses.AddRange(loadedDoctorDiagnoses);
    }

    public override async Task CreateEntity(object entityRequest){
        if (entityRequest is SaveDiagnosisRequest saveDiagnosisRequest) {
            var savedDiagnosis = await UnitOfWork.DiagnosesRepository.SaveDiagnosisAsync(saveDiagnosisRequest);
            Entities.Add(savedDiagnosis);
            _selectedPatientDiagnoses.Add(savedDiagnosis);

            OnEntityCreated(savedDiagnosis);
        }
    }

    public override async Task DeleteEntity(int entityId){
        await UnitOfWork.DiagnosesRepository.DeleteEntityAsync(entityId);
        Entities.RemoveAll(diagnosis => diagnosis.Id == entityId);
        OnEntityDeleted(entityId);
    }

    public override async Task LoadEntityDetails(){
        await Task.CompletedTask; 
    }

    protected override async Task InitializeEntities(){
        var loadedDiagnoses = await UnitOfWork.DiagnosesRepository.GetAllDiagnosesAsync();
        
        Entities.Clear();
        Entities.AddRange(loadedDiagnoses);
    }
}