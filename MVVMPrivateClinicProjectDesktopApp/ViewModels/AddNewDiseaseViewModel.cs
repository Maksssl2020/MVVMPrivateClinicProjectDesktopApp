using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using RelayCommand = MVVMPrivateClinicProjectDesktopApp.Commands.RelayCommand;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class AddNewDiseaseViewModel : ViewModelBase {
    private string _diseaseName = string.Empty;

    [Required(ErrorMessage = "Disease Name is required!")]
    [MinLength(3, ErrorMessage = "Disease Name must be at least 3 characters long!")]
    public string DiseaseName {
        get => _diseaseName;
        set {
            _diseaseName = value;
            Validate(nameof(DiseaseName), value);
            SubmitCommand.OnCanExecuteChanged();
        }
    }

    public RelayCommand SubmitCommand { get; set; }
    private ICommand CreateDiseaseCommand { get; set; }
    
    public AddNewDiseaseViewModel(DiseaseStore diseaseStore){
        SubmitCommand = new SubmitCommand(Submit, CanSubmit);
        CreateDiseaseCommand = new CreateDiseaseCommand(this, diseaseStore, ResetForm);
    }

    private bool CanSubmit(){
        var context = new ValidationContext(this);
        var results = new List<ValidationResult>();
        return Validator.TryValidateObject(this, context, results, true);
    }

    private void Submit(){
        CreateDiseaseCommand.Execute(null);
    }

    private void ResetForm(){
        DiseaseName = string.Empty;
    }
}