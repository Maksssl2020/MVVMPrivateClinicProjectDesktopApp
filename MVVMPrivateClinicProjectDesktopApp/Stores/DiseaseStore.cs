using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class DiseaseStore {
    private readonly IUnitOfWork _unitOfWork;

    private readonly List<Disease> _diseases;
    private readonly Lazy<Task> _initializeLazy;

    public IEnumerable<Disease> Diseases => _diseases;

    public DiseaseStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;

        _diseases = [];
        _initializeLazy = new Lazy<Task>(InitializeDiseases);
    }

    public async Task LoadDiseases(){
        await _initializeLazy.Value;
    }

    private async Task InitializeDiseases(){
        var loadedDiseases = await _unitOfWork.DiseaseRepository.GetAllDiseasesAsync();
        
        _diseases.Clear();
        _diseases.AddRange(loadedDiseases);
    }
}