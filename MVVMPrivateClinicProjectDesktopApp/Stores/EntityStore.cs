using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public abstract class EntityStore<T, TV> : IEntityStore  {
    protected readonly IUnitOfWork UnitOfWork;
    
    public event Action<T>? EntityCreated; 
    public event Action<int>? EntityDeleted;

    protected readonly List<T> Entities;
    private readonly Lazy<Task> _initializeLazy;

    public IEnumerable<T> EntitiesCollection => Entities;
    
    private int _entityIdToShowDetails;
    public int EntityIdToShowDetails {
        get => _entityIdToShowDetails;
        set {
            _entityIdToShowDetails = value;
            SelectedEntityDetails = default!;
        }
    }

    public TV SelectedEntityDetails { get; set; } = default!;
    
    public int EntityIdToDelete { get; set; }
    public string? EntityCode { get; set; }

    protected EntityStore(IUnitOfWork unitOfWork){
        UnitOfWork = unitOfWork;

        Entities = [];
        _initializeLazy = new Lazy<Task>(InitializeEntities);
    }

    public async Task LoadEntities(){
        await _initializeLazy.Value;
    }
    
    protected void OnEntityCreated(T entity){
        EntityCreated?.Invoke(entity);
    }
    
    protected void OnEntityDeleted(int entityId){
        EntityDeleted?.Invoke(entityId);
    }
    
    public abstract Task CreateEntity(object entityRequest);
    public abstract Task DeleteEntity(int entityId);
    public abstract Task LoadEntityDetails();
    protected abstract Task InitializeEntities();
}