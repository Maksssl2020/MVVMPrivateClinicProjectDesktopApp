using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class PricingStore : EntityStore<PricingDto, PricingDetailsDto> {
    private readonly List<ServiceTypeDto> _serviceTypes;
    private readonly List<TopPricingDto> _topPricingDto;
    private readonly Lazy<Task> _initializeServiceTypesLazy;
    private readonly Lazy<Task> _initializeTopPricingLazy;

    public IEnumerable<ServiceTypeDto> ServiceTypesDto => _serviceTypes;
    public IEnumerable<TopPricingDto> TopPricingDto => _topPricingDto;
    
    public PricingStore(IUnitOfWork unitOfWork)
        : base(unitOfWork) {
        _serviceTypes = [];
        _topPricingDto = [];
        _initializeServiceTypesLazy = new Lazy<Task>(InitializeServiceTypesAsync);
        _initializeTopPricingLazy = new Lazy<Task>(InitializeTopPricingAsync);
    }

    public async Task LoadServiceTypesAsync(){
        await _initializeServiceTypesLazy.Value;
    }

    public async Task LoadTopPricingAsync(){
        await _initializeTopPricingLazy.Value;
    }

    private async Task InitializeServiceTypesAsync(){
        var loadedAllServiceTypes = await UnitOfWork.PricingRepository.GetAllServiceTypesDtoAsync();
        
        _serviceTypes.Clear();
        _serviceTypes.AddRange(loadedAllServiceTypes);
    }

    private async Task InitializeTopPricingAsync(){
        var topPricingDtoAsync = await UnitOfWork.PricingRepository.GetTopPricingDtoAsync(5);
        
        _topPricingDto.Clear();
        _topPricingDto.AddRange(topPricingDtoAsync);
    }

    public override async Task CreateEntity(object entityRequest){
        if (entityRequest is SavePricingRequest savePricingRequest) {
            var savedPricing = await UnitOfWork.PricingRepository.SavePricingAsync(savePricingRequest);
            Entities.Add(savedPricing);
            OnEntityCreated(savedPricing);
        }
    }

    public override async Task DeleteEntity(int entityId){
        await UnitOfWork.PricingRepository.DeleteEntityAsync(entityId);
        Entities.RemoveAll(e => e.Id == entityId);
        OnEntityDeleted(entityId);
    }

    public override async Task LoadEntityDetails(){
        var foundPricingDetails = await UnitOfWork.PricingRepository.GetPricingDetailsByIdAsync(EntityIdToShowDetails);

        if (foundPricingDetails != null) {
            SelectedEntityDetails = foundPricingDetails;
        }
    }

    protected override async Task InitializeEntities(){
        var foundPricing = await UnitOfWork.PricingRepository.GetAllPricingDtoAsync();
        
        Entities.Clear();
        Entities.AddRange(foundPricing);
    }
}