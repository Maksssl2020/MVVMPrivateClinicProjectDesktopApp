using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class DiseaseStore {
    private readonly IUnitOfWork _unitOfWork;

    private readonly List<DiseaseDto> _diseasesDto;
    private readonly Lazy<Task> _initializeLazy;

    public IEnumerable<DiseaseDto> DiseasesDto => _diseasesDto;

    public event Action<DiseaseDto>? DiseaseCreated;
    
    public DiseaseStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;

        _diseasesDto = [];
        _initializeLazy = new Lazy<Task>(InitializeDiseases);
    }

    public async Task LoadDiseases(){
        await _initializeLazy.Value;
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