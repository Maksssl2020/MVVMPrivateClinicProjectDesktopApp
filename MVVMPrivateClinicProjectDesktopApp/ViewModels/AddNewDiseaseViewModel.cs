using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using RelayCommand = MVVMPrivateClinicProjectDesktopApp.Commands.RelayCommand;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class AddNewDiseaseViewModel : ViewModelBase {
    private string _diseaseName = string.Empty;

    [Required(ErrorMessage = "Disease Name is required!")]
    public string DiseaseName {
        get => _diseaseName;
        set {
            _diseaseName = value;
            Validate(nameof(DiseaseName), value);
            SubmitCommand.OnCanExecuteChanged();
        }
    }

    public RelayCommand SubmitCommand { get; set; }

    public AddNewDiseaseViewModel(DiseaseStore diseaseStore){
        SubmitCommand = new SubmitCommand(Submit, CanSubmit);
    }

    private bool CanSubmit(){
        var context = new ValidationContext(this);
        var results = new List<ValidationResult>();
        return Validator.TryValidateObject(this, context, results, true);
    }

    private void Submit(){
        throw new NotImplementedException();
    }
}