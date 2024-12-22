using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

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
    
    protected ICommand LoadEntityCommand { get; set; } = loadEntityCommand;
}