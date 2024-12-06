using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class PrescriptionStore {
    private readonly IUnitOfWork _unitOfWork;

    private readonly List<PrescriptionDto> _prescriptionsDto;
    private readonly Lazy<Task> _initializeLazy;

    public IEnumerable<PrescriptionDto> PrescriptionsDto => _prescriptionsDto;
    
    public PrescriptionStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;
        _prescriptionsDto = [];
        _initializeLazy = new Lazy<Task>(InitializePrescriptions);

    }

    public async Task LoadPrescriptions(){
        await _initializeLazy.Value;
    }

    private async Task InitializePrescriptions(){
        var prescriptionsDto = await _unitOfWork.PrescriptionRepository.GetAllPrescriptionsDtoAsync();
        
        _prescriptionsDto.Clear();
        _prescriptionsDto.AddRange(prescriptionsDto);
    }
}