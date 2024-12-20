using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class PricingStore {
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly List<PricingDto> _pricingDto;
    private readonly Lazy<Task> _initializeAllPricingLazy;
    private readonly List<ServiceTypeDto> _serviceTypes;
    private readonly Lazy<Task> _initializeServiceTypesLazy;
    private readonly List<TopPricingDto> _topPricingDto;
    private readonly Lazy<Task> _initializeTopPricingLazy;

    public IEnumerable<PricingDto> PricingDto => _pricingDto;
    public IEnumerable<ServiceTypeDto> ServiceTypesDto => _serviceTypes;
    public IEnumerable<TopPricingDto> TopPricingDto => _topPricingDto;
    
    public event Action<PricingDto>? PricingCreated;
    
    public PricingStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;

        _pricingDto = [];
        _serviceTypes = [];
        _topPricingDto = [];
        _initializeAllPricingLazy = new Lazy<Task>(InitializePricingAsync);
        _initializeServiceTypesLazy = new Lazy<Task>(InitializeServiceTypesAsync);
        _initializeTopPricingLazy = new Lazy<Task>(InitializeTopPricingAsync);
    }

    public async Task LoadPricingAsync(){
        await _initializeAllPricingLazy.Value;
    }

    public async Task LoadServiceTypesAsync(){
        await _initializeServiceTypesLazy.Value;
    }

    public async Task LoadTopPricingAsync(){
        await _initializeTopPricingLazy.Value;
    }
    
    public async Task CreatePricingAsync(SavePricingRequest pricingRequest){
        var savedPricing = await _unitOfWork.PricingRepository.SavePricingAsync(pricingRequest);
        _pricingDto.Add(savedPricing);
        
        OnPricingCreated(savedPricing);
    }

    private void OnPricingCreated(PricingDto pricingDto){
        PricingCreated?.Invoke(pricingDto);
    }
    
    private async Task InitializePricingAsync(){
        var foundPricing = await _unitOfWork.PricingRepository.GetAllPricingDtoAsync();
        
        _pricingDto.Clear();
        _pricingDto.AddRange(foundPricing);
    }

    private async Task InitializeServiceTypesAsync(){
        var loadedAllServiceTypes = await _unitOfWork.PricingRepository.GetAllServiceTypesDtoAsync();
        
        _serviceTypes.Clear();
        _serviceTypes.AddRange(loadedAllServiceTypes);
    }

    private async Task InitializeTopPricingAsync(){
        var topPricingDtoAsync = await _unitOfWork.PricingRepository.GetTopPricingDtoAsync(5);
        
        _topPricingDto.Clear();
        _topPricingDto.AddRange(topPricingDtoAsync);
    }
}