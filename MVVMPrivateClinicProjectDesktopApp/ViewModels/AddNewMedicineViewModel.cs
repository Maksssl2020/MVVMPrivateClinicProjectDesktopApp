using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class AddNewMedicineViewModel : ViewModelBase {
    private readonly ObservableCollection<MedicineTypeDto> _medicineTypes;
    public ICollectionView MedicineTypes { get; set; }

    private string _medicineName = string.Empty;
    
    [Required(ErrorMessage = "Medicine Name is required!")]
    [MinLength(3, ErrorMessage = "Medicine Name must be at least 3 characters long!")]
    [RegularExpression(@"([\p{L}]+[\s]?)+", ErrorMessage = "Use letters only please!")]
    public string MedicineName {
        get => _medicineName;
        set {
            _medicineName = value;
            Validate(nameof(MedicineName), value);
            SubmitCommand.OnCanExecuteChanged();
        }
    }

    private string _medicineType = string.Empty;

    [Required(ErrorMessage = "Medicine Type is required!")]
    [RegularExpression(@"([\p{L}]+[\s]?)+", ErrorMessage = "Use letters only please!")]
    public string MedicineType {
        get => _medicineType;
        set {
            _medicineType = value;
            Validate(nameof(MedicineType), value);
            SubmitCommand.OnCanExecuteChanged();
        }
    }
    
    private string _medicineDescription = string.Empty;
    
    [Required(ErrorMessage = "Medicine Description is required!")]
    [MinLength(10, ErrorMessage = "Medicine Description must be at least 10 characters long!")]
    [RegularExpression(@"([\p{L}]+[\s]?)+", ErrorMessage = "Use letters only please!")]
    public string MedicineDescription {
        get => _medicineDescription;
        set {
            _medicineDescription = value;
            Validate(nameof(MedicineDescription), value);
            SubmitCommand.OnCanExecuteChanged();
        }
    }
    
    public SubmitCommand SubmitCommand { get; set; }
    private ICommand LoadMedicineTypesCommand { get; set; }
    public ICommand CreateMedicineCommand { get; set; }
    
    private AddNewMedicineViewModel(MedicineStore medicineStore) {
        _medicineTypes = [];
        
        MedicineTypes = CollectionViewSource.GetDefaultView(_medicineTypes);

        SubmitCommand = new SubmitCommand(Submit, CanSubmit);
        LoadMedicineTypesCommand = new LoadMedicineTypesCommand(this, medicineStore);
        CreateMedicineCommand = new CreateMedicineCommand(this, medicineStore);
    }
    
    public static AddNewMedicineViewModel LoadAddNewMedicineViewModel(MedicineStore medicineStore){
        var addNewMedicineViewModel = new AddNewMedicineViewModel(medicineStore);
        
        addNewMedicineViewModel.LoadMedicineTypesCommand.Execute(null);
        
        return addNewMedicineViewModel;
    }
    
    public void UpdateMedicineTypes(IEnumerable<MedicineTypeDto> medicineTypes){
        _medicineTypes.Clear();
        
        foreach (var medicineType in medicineTypes) {
            _medicineTypes.Add(medicineType);
            Console.WriteLine(medicineType.Type);
        }
    }
    
    private bool CanSubmit(){
        var context = new ValidationContext(this);
        var results = new List<ValidationResult>();
        return Validator.TryValidateObject(this, context, results, true);
    }

    private void Submit(){
        CreateMedicineCommand.Execute(null);
        ResetForm();
    }

    private void ResetForm(){
        MedicineName = string.Empty;
        MedicineDescription = string.Empty;
        MedicineType = string.Empty;
    }
}