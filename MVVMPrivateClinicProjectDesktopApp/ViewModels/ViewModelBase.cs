using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class ViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo {
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null){
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private readonly Dictionary<string, List<string>> _errors = new();
    
    public bool HasErrors => _errors.Count > 0;
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
    
    public IEnumerable GetErrors(string? propertyName){
        if (propertyName != null && _errors.TryGetValue(propertyName, out var errors)) {
            return errors;
        }

        return Enumerable.Empty<string>();
    }

    protected void Validate(string propertyName, object propertyValue){
        var results = new List<ValidationResult>();
        var validationContext = new ValidationContext(this) {MemberName = propertyName};
        

        _errors.Remove(propertyName);
        
        var isValid = Validator.TryValidateProperty(propertyValue, validationContext, results);
        
        if (!isValid) {
            _errors[propertyName] = results.Select(result => result.ErrorMessage!).ToList();
        }
        
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}