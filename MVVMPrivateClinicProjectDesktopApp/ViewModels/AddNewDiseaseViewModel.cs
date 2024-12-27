using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class AddNewDiseaseViewModel : AddNewEntityViewModelBase {
    private string _diseaseName = string.Empty;

    [Required(ErrorMessage = "Disease Name is required!")]
    [MinLength(3, ErrorMessage = "Disease Name must be at least 3 characters long!")]
    public string DiseaseName {
        get => _diseaseName;
        set {
            _diseaseName = value;
            Validate(nameof(DiseaseName), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }

    private ICommand CreateDiseaseCommand { get; set; }
    
    public AddNewDiseaseViewModel(DiseaseStore diseaseStore){
        SubmitCommand = new SubmitCommand(Submit, CanSubmit);
        CreateDiseaseCommand = new CreateDiseaseCommand(this, diseaseStore, ResetForm);
    }

    protected override void Submit(){
        CreateDiseaseCommand.Execute(null);
    }

    protected override void ResetForm(){
        DiseaseName = string.Empty;
    }
}