using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class IssuePrescriptionViewModel : ViewModelBase, IMedicinesViewModel, IDoctorsViewModel {

    private readonly ObservableCollection<MedicineDto> _medicinesDto;
    private readonly ObservableCollection<DoctorDto> _doctorsDto;
    public ICollectionView MedicinesDtoView { get; set; }
    public ICollectionView DoctorsDtoView { get; set; }
    
    private DoctorDto _selectedDoctor = null!;
    
    [Required(ErrorMessage = "Doctor is required!")]
    public DoctorDto SelectedDoctor {
        get => _selectedDoctor;
        set {
            _selectedDoctor = value;
            Validate(nameof(SelectedDoctor), value);
            SubmitCommand.OnCanExecuteChanged();
        }
    }
    
    private string _prescriptionDescription = string.Empty;

    [Required(ErrorMessage = "Prescription Description is required!")]
    [RegularExpression(@"([\p{L}]+[\s]?)+", ErrorMessage = "Use letters only please!")]
    public string PrescriptionDescription {
        get => _prescriptionDescription;
        set {
            _prescriptionDescription = value;
            Validate(nameof(PrescriptionDescription), value);
            SubmitCommand.OnCanExecuteChanged();
        }
    }
    
    private List<MedicineDto> _selectedMedicines = [];

    [Required(ErrorMessage = "Medicines are required!")]
    public List<MedicineDto> SelectedMedicines {
        get => _selectedMedicines;
        set {
            _selectedMedicines = value;
            Validate(nameof(SelectedMedicines), value);
            SubmitCommand.OnCanExecuteChanged();

            Console.WriteLine(_selectedMedicines.Count);
        }
    }
    
    private ICommand LoadMedicinesDtoCommand { get; set; }
    private ICommand LoadDoctorsCommand { get; set; }
    public SubmitCommand SubmitCommand { get; set; }

    public static string Today => DateTime.Today.ToString("dd-MM-yyyy");
    
    private IssuePrescriptionViewModel(MedicineStore medicineStore, DoctorStore doctorStore){
        _medicinesDto = [];
        _doctorsDto = [];

        LoadMedicinesDtoCommand = new LoadMedicinesDtoCommand(this, medicineStore);
        LoadDoctorsCommand = new LoadFamilyDoctorsCommand(this, doctorStore);
        SubmitCommand = new SubmitCommand(Submit, CanSubmit);
        
        MedicinesDtoView = CollectionViewSource.GetDefaultView(_medicinesDto);
        DoctorsDtoView = CollectionViewSource.GetDefaultView(_doctorsDto);
    }
    
    private bool CanSubmit(){
        var context = new ValidationContext(this);
        var results = new List<ValidationResult>();
        return Validator.TryValidateObject(this, context, results, true);
    }
    
    private void Submit(){
        Console.WriteLine("SUBMIT!");
    }

    public static IssuePrescriptionViewModel LoadIssuePrescriptionViewModel(MedicineStore medicineStore, DoctorStore doctorStore){
        var viewModel = new IssuePrescriptionViewModel(medicineStore, doctorStore);
        
        viewModel.LoadMedicinesDtoCommand.Execute(null);
        viewModel.LoadDoctorsCommand.Execute(null);

        return viewModel;
    }

    public void UpdateMedicines(IEnumerable<MedicineDto> medicinesDto) {
        _medicinesDto.Clear();

        foreach (var medicineDto in medicinesDto) {
            _medicinesDto.Add(medicineDto);
        }
    }

    public void UpdateDoctorsDto(IEnumerable<DoctorDto> doctorsDto){
        _doctorsDto.Clear();
        
        foreach (var doctorDto in doctorsDto) {
            _doctorsDto.Add(doctorDto);
        }
    }
}