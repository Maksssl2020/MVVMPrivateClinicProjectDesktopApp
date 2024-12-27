using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.IdentityModel.Tokens;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using static System.Enum;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class IssuePrescriptionViewModel : AddNewEntityViewModelBase {
    public static string Today => DateTime.Today.ToString("dd-MM-yyyy");

    private readonly ObservableCollection<MedicineDto> _medicinesDto;
    private readonly ObservableCollection<DoctorDto> _doctorsDto;
    public ICollectionView MedicinesDtoView { get; }
    public ICollectionView DoctorsDtoView { get; }
    
    private DoctorDto _selectedDoctor = null!;
    
    [Required(ErrorMessage = "Doctor is required!")]
    public DoctorDto SelectedDoctor {
        get => _selectedDoctor;
        set {
            _selectedDoctor = value;
            Validate(nameof(SelectedDoctor), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }
    
    private string _prescriptionDescription = string.Empty;

    [Required(ErrorMessage = "Prescription Description is required!")]
    public string PrescriptionDescription {
        get => _prescriptionDescription;
        set {
            _prescriptionDescription = value;
            Validate(nameof(PrescriptionDescription), value);
            SubmitCommand.OnCanExecuteChanged();
            OnPropertyChanged();
        }
    }
    
    private List<MedicineDto> _selectedMedicines = [];

    [MinLength(1, ErrorMessage = "Medicines are required!")]
    public List<MedicineDto> SelectedMedicines {
        get => _selectedMedicines;
        set {
            _selectedMedicines = value;
            Validate(nameof(SelectedMedicines), value);
            SubmitCommand.OnCanExecuteChanged();

            if (!_errors.TryGetValue(nameof(SelectedMedicines), out var error)) {
                SelectedMedicinesError = string.Empty;
                return;
            }

            if (error.Count != 0) {
                SelectedMedicinesError = error[0];
            }
            
            OnPropertyChanged();
        }
    }

    private string _selectedMedicinesError = string.Empty;
    public string SelectedMedicinesError {
        get => _selectedMedicinesError;
        private set {
            _selectedMedicinesError = value;
            OnPropertyChanged();
        }
    }

    private readonly int _patientId;
    public int SelectedPatientId {
        get => _patientId;
        private init {
            _patientId = value;
            OnPropertyChanged();
        }
    }
    
    private PrescriptionValidity _prescriptionValidity = PrescriptionValidity.OneMonth;
    public PrescriptionValidity PrescriptionValidity {
        get => _prescriptionValidity;
        private set {
            _prescriptionValidity = value;
            OnPropertyChanged();
        }
    }
    
    private ICommand LoadMedicinesDtoCommand { get; set; }
    private ICommand LoadDoctorsCommand { get; set; }
    public ICommand SetPrescriptionValidityCommand { get; set; }
    private ICommand CreatePrescriptionCommand { get; set; }
    
    private IssuePrescriptionViewModel(MedicineStore medicineStore, DoctorStore doctorStore, PatientStore patientStore, PrescriptionStore prescriptionStore){
        _medicinesDto = [];
        _doctorsDto = [];
        SelectedPatientId = patientStore.EntityIdToShowDetails;

        LoadMedicinesDtoCommand = new LoadEntitiesCommand<MedicineDto, MedicineDetailsDto>(UpdateMedicines, medicineStore);
        LoadDoctorsCommand = new LoadFamilyDoctorsCommand(UpdateDoctorsDto, doctorStore);
        SetPrescriptionValidityCommand = new RelayCommand<string>(SetPrescriptionValidity);
        CreatePrescriptionCommand = new CreatePrescriptionCommand(this, prescriptionStore, ResetForm);
        
        MedicinesDtoView = CollectionViewSource.GetDefaultView(_medicinesDto);
        DoctorsDtoView = CollectionViewSource.GetDefaultView(_doctorsDto);
    }
    
    public static IssuePrescriptionViewModel LoadIssuePrescriptionViewModel(MedicineStore medicineStore, DoctorStore doctorStore, PatientStore patientStore, PrescriptionStore prescriptionStore){
        var viewModel = new IssuePrescriptionViewModel(medicineStore, doctorStore, patientStore, prescriptionStore);
        
        viewModel.LoadMedicinesDtoCommand.Execute(null);
        viewModel.LoadDoctorsCommand.Execute(null);

        return viewModel;
    }
    
    protected override void Submit(){
        CreatePrescriptionCommand.Execute(null);
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

    private void SetPrescriptionValidity(string? validity){
        if (validity.IsNullOrEmpty()) {
            return;
        }

        var result = TryParse(validity, out PrescriptionValidity prescriptionValidity);

        if (result) {
            PrescriptionValidity = prescriptionValidity;
        }
    }

    protected override void ResetForm(){
        SelectedDoctor = null!;
        SelectedMedicines.Clear();
        PrescriptionDescription = string.Empty;
        PrescriptionValidity = PrescriptionValidity.OneMonth;
        SelectedMedicinesError = string.Empty;
    }
}