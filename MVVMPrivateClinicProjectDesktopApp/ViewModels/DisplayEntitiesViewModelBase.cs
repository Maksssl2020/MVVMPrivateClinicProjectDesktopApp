using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using MVVMPrivateClinicProjectDesktopApp.Helpers;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public abstract class DisplayEntitiesViewModelBase<T> : ViewModelBase {
    protected ObservableCollection<T> Entities { get; set; }
    public ObservableCollection<SortingOptions> SortingOptionsList { get; }
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

    protected DisplayEntitiesViewModelBase(ObservableCollection<SortingOptions> sortingOptionsList) {
        SortingOptionsList = sortingOptionsList;
        Entities = [];
        
        EntitiesView = CollectionViewSource.GetDefaultView(Entities);
        EntitiesView.Filter = ApplyFilter;
    }

    public abstract void UpdateEntities(IEnumerable<T> entities);
    protected abstract void SortEntities();
    protected abstract bool ApplyFilter(object obj);
}