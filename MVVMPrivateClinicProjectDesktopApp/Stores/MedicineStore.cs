using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class MedicineStore {
    private readonly IUnitOfWork _unitOfWork;

    private readonly List<MedicineDto> _medicinesDto;
    private readonly Lazy<Task> _initializeLazy;

    public IEnumerable<MedicineDto> MedicinesDto => _medicinesDto;
    
    public MedicineStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;
        _medicinesDto = [];
        _initializeLazy = new Lazy<Task>(InitializeMedicinesDto);
    }

    public async Task LoadMedicinesDto(){
        await _initializeLazy.Value;
    }
    
    private async Task InitializeMedicinesDto(){
        var loadedMedicinesDto = await _unitOfWork.MedicineRepository.GetAllMedicinesDtoAsync();
        
        _medicinesDto.Clear();
        _medicinesDto.AddRange(loadedMedicinesDto);
    }
}