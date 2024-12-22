using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class DiagnosisStore {
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly List<DiagnosisDto> _diagnoses;
    private readonly List<DiagnosisDto> _selectedPatientDiagnoses;
    private readonly List<DiagnosisDto> _doctorIssuedDiagnoses;
    private readonly Lazy<Task> _initializeDiagnoses;

    public IEnumerable<DiagnosisDto> Diagnoses => _diagnoses;
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
    
    public event Action<DiagnosisDto>? DiagnosisCreated;

    public DiagnosisStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;
        
        _diagnoses = [];
        _selectedPatientDiagnoses = [];
        _doctorIssuedDiagnoses = [];
        _initializeDiagnoses = new Lazy<Task>(InitializeDiagnoses);
    }

    public async Task LoadDiagnoses(){
        await _initializeDiagnoses.Value;
    }

    public async Task LoadPatientDiagnoses(){
        var loadedPatientDiagnoses = await _unitOfWork.DiagnosisRepository.GetIssuedDiagnosesByPatientIdOrDoctorId(SelectedPatientId, PersonType.Patient);
        _selectedPatientDiagnoses.AddRange(loadedPatientDiagnoses);
    }

    public async Task LoadDoctorIssuedDiagnoses(){
        var loadedDoctorDiagnoses = await _unitOfWork.DiagnosisRepository.GetIssuedDiagnosesByPatientIdOrDoctorId(SelectedPatientId, PersonType.Doctor);
        _doctorIssuedDiagnoses.AddRange(loadedDoctorDiagnoses);
    }
    
    public async Task CreateDiagnosis(SaveDiagnosisRequest diagnosisRequest){
        var savedDiagnosis = await _unitOfWork.DiagnosisRepository.SaveDiagnosisAsync(diagnosisRequest);
        _diagnoses.Add(savedDiagnosis);
        _selectedPatientDiagnoses.Add(savedDiagnosis);

        OnDiagnosisCreated(savedDiagnosis);
    }

    private void OnDiagnosisCreated(DiagnosisDto savedDiagnosis){
        DiagnosisCreated?.Invoke(savedDiagnosis);
    }   

    private async Task InitializeDiagnoses(){
        var loadedDiagnoses = await _unitOfWork.DiagnosisRepository.GetAllDiagnosisAsync();
        
        _diagnoses.Clear();
        _diagnoses.AddRange(loadedDiagnoses);
    }
}