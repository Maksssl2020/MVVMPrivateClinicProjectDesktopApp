using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class MedicineStore {
    private readonly IUnitOfWork _unitOfWork;

    private readonly List<MedicineDto> _medicinesDto;
    private readonly Lazy<Task> _initializeMedicinesLazy;

    private readonly List<MedicineTypeDto> _medicineTypes;
    private readonly Lazy<Task> _initializeMedicineTypesLazy;
    
    public IEnumerable<MedicineDto> MedicinesDto => _medicinesDto;
    public IEnumerable<MedicineTypeDto> MedicineTypes => _medicineTypes;

    private int _selectedMedicineId;
    public int SelectedMedicineId {
        get => _selectedMedicineId;
        set {
            _selectedMedicineId = value;
            MedicineDetails = null!;
        }
    }

    public MedicineDetailsDto MedicineDetails { get; set; } = null!;
    
    public event Action<MedicineDto>? MedicineCreated; 
    
    public MedicineStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;
        _medicinesDto = [];
        _medicineTypes = [];
        _initializeMedicinesLazy = new Lazy<Task>(InitializeMedicinesDto);
        _initializeMedicineTypesLazy = new Lazy<Task>(InitializeMedicineTypes);
    }

    public async Task LoadMedicinesDto(){
        await _initializeMedicinesLazy.Value;
    }

    public async Task LoadMedicineTypes(){
        await _initializeMedicineTypesLazy.Value;
    }

    public async Task CreateMedicine(SaveMedicineRequest medicineRequest){
        var savedMedicine = await _unitOfWork.MedicineRepository.SaveMedicineAsync(medicineRequest);
        _medicinesDto.Add(savedMedicine);
        

        var exists = _medicineTypes.Exists(medicineTypeDto => medicineTypeDto.Type == savedMedicine.Type);
        if (!exists) {
            _medicineTypes.Add(new MedicineTypeDto { Type = savedMedicine.Type });
        }
        
        OnMedicineCreated(savedMedicine);
    }

    public async Task LoadMedicine(){
        var foundMedicine = await _unitOfWork.MedicineRepository.GetMedicineByIdAsync(SelectedMedicineId);
        var medicineUses = await _unitOfWork.PrescriptionRepository.CountMedicineUsesAsync(SelectedMedicineId);
        foundMedicine.TotalUses = medicineUses;
        MedicineDetails = foundMedicine;
    }
    
    private void OnMedicineCreated(MedicineDto medicine){
        MedicineCreated?.Invoke(medicine);
    }
    
    private async Task InitializeMedicinesDto(){
        var loadedMedicinesDto = await _unitOfWork.MedicineRepository.GetAllMedicinesDtoAsync();
        
        _medicinesDto.Clear();
        _medicinesDto.AddRange(loadedMedicinesDto);
    }

    private async Task InitializeMedicineTypes(){
        var loadedMedicineTypes = await _unitOfWork.MedicineRepository.GetAllExistingMedicineTypesAsync();
        
        _medicineTypes.Clear();
        _medicineTypes.AddRange(loadedMedicineTypes);
    }
}