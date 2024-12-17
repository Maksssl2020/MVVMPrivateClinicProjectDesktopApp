using System.ComponentModel.DataAnnotations;
using MVVMPrivateClinicProjectDesktopApp.Commands;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public abstract class AddNewEntityViewModelBase : ViewModelBase {
    public SubmitCommand SubmitCommand { get; set; }

    protected AddNewEntityViewModelBase(){
        SubmitCommand = new SubmitCommand(Submit, CanSubmit);
    }

    private bool CanSubmit(){
        var context = new ValidationContext(this);
        var results = new List<ValidationResult>();
        return Validator.TryValidateObject(this, context, results, true);
    }

    protected abstract void Submit();
    protected abstract void ResetForm();
}