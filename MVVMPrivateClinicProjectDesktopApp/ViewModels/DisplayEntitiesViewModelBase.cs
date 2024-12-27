using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public abstract class DisplayEntitiesViewModelBase<T, TV> : ViewModelBase where T : class, IEntity {
    protected readonly EntityStore<T, TV> EntityStore;
    public ObservableCollection<SortingOptions> SortingOptionsList { get; }
    protected ObservableCollection<T> Entities { get; set; }
    public ICollectionView EntitiesView { get; set; }
    
    private string _filter = string.Empty;
    public string Filter {
        get => _filter;
        set {
            _filter = value;
            OnPropertyChanged();
            EntitiesView.Refresh();
        }
    }
    
    private SortingOptions _selectedSortingOption;
    public SortingOptions SelectedSortingOption {
        get => _selectedSortingOption;
        set {
            _selectedSortingOption = value;
            OnPropertyChanged();
            SortEntities();
        }
    }

    protected ICommand LoadEntitiesCommand { get; set; }
    private ICommand? ShowDeleteEntityModalCommand { get; set; }
    
    protected DisplayEntitiesViewModelBase(
        ObservableCollection<SortingOptions> sortingOptionsList,
        EntityStore<T, TV> entityStore,
        ModalNavigationViewModel? modalNavigationViewModel
        ) {
        SortingOptionsList = sortingOptionsList;
        EntityStore = entityStore;
        Entities = [];
        
        EntitiesView = CollectionViewSource.GetDefaultView(Entities);
        EntitiesView.Filter = ApplyFilter;

        LoadEntitiesCommand = new LoadEntitiesCommand<T, TV>(UpdateEntities, entityStore);
        ShowDeleteEntityModalCommand = modalNavigationViewModel?.ShowDeleteEntityModal;
        
        entityStore.EntityCreated += OnEntityCreated;
        entityStore.EntityDeleted += OnEntityDeleted;
    }

    protected abstract void UpdateEntities(IEnumerable<T> entities);
    protected abstract void SortEntities();
    protected abstract bool ApplyFilter(object obj);

    public void SetEntityIdToShowDetails(int entityId){
        EntityStore.EntityIdToShowDetails = entityId;
    }

    public void SetEntityIdToDelete(int entityId){
        EntityStore.EntityIdToDelete = entityId;
        ShowDeleteEntityModalCommand?.Execute(EntityStore);
    }
    
    protected void OnEntityCreated(T entity){
        Entities.Add(entity);
    }

    private void OnEntityDeleted(int entityId){
        var foundEntity = Entities.First(e => e.Id == entityId);
        Entities.Remove(foundEntity);
    }
}