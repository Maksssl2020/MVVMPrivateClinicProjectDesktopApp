using System.Windows.Input;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public abstract class EntityDetailsViewModelBase<T>(ICommand loadEntityCommand) : ViewModelBase
    where T : class {
    
    private T _entity = null!;
    public T Entity {
        get => _entity;
        set {
            _entity = value;
            OnPropertyChanged();
        }
    }

    public void UpdateEntity(T entity) {
        Entity = entity;
    }
    
    protected ICommand LoadEntityCommand { get; } = loadEntityCommand;
}