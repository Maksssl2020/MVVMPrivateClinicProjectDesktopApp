using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class DiseaseStore {
    private readonly IUnitOfWork _unitOfWork;

    private readonly List<DiseaseDto> _diseasesDto;
    private readonly Lazy<Task> _initializeLazy;

    public IEnumerable<DiseaseDto> DiseasesDto => _diseasesDto;

    private int _selectedDiseaseId;

    public int SelectedDiseaseId {
        get => _selectedDiseaseId;
        set {
            _selectedDiseaseId = value;
            DiseaseDetails = null!;
        }
    }

    public DiseaseDetailsDto DiseaseDetails { get; set; } = null!;
    
    public event Action<DiseaseDto>? DiseaseCreated;
    
    public DiseaseStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;

        _diseasesDto = [];
        _initializeLazy = new Lazy<Task>(InitializeDiseases);
    }

    public async Task LoadDiseases(){
        await _initializeLazy.Value;
    }

    public async Task LoadDisease(){
        var foundDisease = await _unitOfWork.DiseaseRepository.GetDiseaseDetailsByIdAsync(SelectedDiseaseId);
        var countDiagnosedDiseaseAsync = await _unitOfWork.DiagnosisRepository.CountDiagnosedDiseaseAsync(SelectedDiseaseId);

        if (foundDisease != null) {
            foundDisease.TotalDiagnoses = countDiagnosedDiseaseAsync;
            DiseaseDetails = foundDisease;
        }
    }
    
    public async Task CreateDisease(SaveDiseaseRequest diseaseRequest){
        var savedDisease = await _unitOfWork.DiseaseRepository.SaveDiseaseAsync(diseaseRequest);
        _diseasesDto.Add(savedDisease);
        
        OnDiseaseCreated(savedDisease);
    }
    
    private void OnDiseaseCreated(DiseaseDto diseaseDto){
        DiseaseCreated?.Invoke(diseaseDto);
    }
    
    private async Task InitializeDiseases(){
        var loadedDiseases = await _unitOfWork.DiseaseRepository.GetAllDiseasesAsync();
        
        _diseasesDto.Clear();
        _diseasesDto.AddRange(loadedDiseases);
    }
}