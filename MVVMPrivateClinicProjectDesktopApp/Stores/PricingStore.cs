using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class PricingStore {
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly List<PricingDto> _pricingDto;
    private readonly Lazy<Task> _initializeLazy;

    public IEnumerable<PricingDto> PricingDto => _pricingDto;

    public PricingStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;

        _pricingDto = [];
        _initializeLazy = new Lazy<Task>(InitializePricingAsync);
    }

    public async Task LoadPricingAsync(){
        await _initializeLazy.Value;
    }

    private async Task InitializePricingAsync(){
        var foundPricing = await _unitOfWork.PricingRepository.GetAllPricingDtoAsync();
        
        _pricingDto.Clear();
        _pricingDto.AddRange(foundPricing);
    }
}