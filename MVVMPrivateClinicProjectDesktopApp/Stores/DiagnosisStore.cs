using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class DiagnosisStore {
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly List<DiagnosisDto> _diagnoses;
    private readonly List<DiagnosisDto> _selectedPatientDiagnoses;
    private readonly Lazy<Task> _initializeDiagnoses;

    public IEnumerable<DiagnosisDto> Diagnoses => _diagnoses;
    public IEnumerable<DiagnosisDto> SelectedPatientDiagnoses => _selectedPatientDiagnoses;

    private int _selectedPatientId;
    public int SelectedPatientId {
        get => _selectedPatientId;
        set {
            _selectedPatientId = value;
            _selectedPatientDiagnoses.Clear();
        }
    }
    
    public event Action<DiagnosisDto>? DiagnosisCreated;

    public DiagnosisStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;
        
        _diagnoses = [];
        _selectedPatientDiagnoses = [];
        _initializeDiagnoses = new Lazy<Task>(InitializeDiagnoses);
    }

    public async Task LoadDiagnoses(){
        await _initializeDiagnoses.Value;
    }

    public async Task LoadPatientDiagnoses(){
        var loadedPatientDiagnoses = await _unitOfWork.DiagnosisRepository.GetPatientAllDiagnosisAsync(SelectedPatientId);
        _selectedPatientDiagnoses.AddRange(loadedPatientDiagnoses);
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