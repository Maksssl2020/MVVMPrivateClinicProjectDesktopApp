using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class IssuePrescriptionViewModel : ViewModelBase {
    private readonly MedicineStore _medicineStore;
    private readonly DoctorStore _doctorStore;
    
    private DoctorDto _selectedDoctor = null!;

    public DoctorDto SelectedDoctor {
        get => _selectedDoctor;
        set {
            _selectedDoctor = value;
            OnPropertyChanged();
            Console.WriteLine(_selectedDoctor.FirstName);
        }
    }

    private ObservableCollection<MedicineDto> _medicinesDto {get; set;}
    private ObservableCollection<DoctorDto> _doctorsDto {get; set;}
    
    public ICollectionView MedicinesDtoView { get; set; }
    public ICollectionView DoctorsDtoView { get; set; }

    public bool IsOpen { get; set; } = false;

    private ICommand LoadMedicinesDtoCommand { get; set; }
    private ICommand LoadDoctorsCommand { get; set; }

    public static string Today => DateTime.Today.ToString("dd-MM-yyyy");
    
    private IssuePrescriptionViewModel(MedicineStore medicineStore, DoctorStore doctorStore){
        _medicineStore = medicineStore;
        _doctorStore = doctorStore;
        _medicinesDto = [];
        _doctorsDto = [];

        LoadMedicinesDtoCommand = new LoadMedicinesDtoCommand(this, _medicineStore);
        LoadDoctorsCommand = new LoadFamilyDoctorsCommand(this, _doctorStore);
        
        MedicinesDtoView = CollectionViewSource.GetDefaultView(_medicinesDto);
        DoctorsDtoView = CollectionViewSource.GetDefaultView(_doctorsDto);
    }

    public static IssuePrescriptionViewModel LoadIssuePrescriptionViewModel(MedicineStore medicineStore, DoctorStore doctorStore){
        var viewModel = new IssuePrescriptionViewModel(medicineStore, doctorStore);
        
        viewModel.LoadMedicinesDtoCommand.Execute(null);
        viewModel.LoadDoctorsCommand.Execute(null);

        return viewModel;
    }

    public void UpdateMedicinesDto(IEnumerable<MedicineDto> medicinesDto) {
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

    public void ToggleIsOpen(){
        IsOpen = !IsOpen;
    }
}