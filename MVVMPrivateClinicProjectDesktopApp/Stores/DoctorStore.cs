using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class DoctorStore {
    private readonly IUnitOfWork _unitOfWork;

    private readonly List<DoctorDto> _allDoctorsDto;
    private readonly List<DoctorDto> _familyMedicineDoctorsDto;
    private readonly Lazy<Task> _initializeLazyAllDoctors;
    private readonly Lazy<Task> _initializeLazyFamilyMedicineDoctors;

    public IEnumerable<DoctorDto> AllDoctorsDto => _allDoctorsDto;
    public IEnumerable<DoctorDto> FamilyMedicineDoctorsDto => _familyMedicineDoctorsDto;
    
    public DoctorStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;
        _allDoctorsDto = [];
        _familyMedicineDoctorsDto = [];
        _initializeLazyAllDoctors = new Lazy<Task>(InitializeAllDoctorsDto);
        _initializeLazyFamilyMedicineDoctors = new Lazy<Task>(InitializeFamilyMedicineDoctorsDto);
    }

    public async Task LoadAllDoctorsDto(){
        await _initializeLazyAllDoctors.Value;
    }

    public async Task LoadFamilyMedicineDoctorsDto(){
        await _initializeLazyFamilyMedicineDoctors.Value;
    }
    
    private async Task InitializeAllDoctorsDto(){
        var loadedDoctors = await _unitOfWork.DoctorRepository.GetAllDoctors();
        
        _allDoctorsDto.Clear();
        _allDoctorsDto.AddRange(loadedDoctors);
    }

    private async Task InitializeFamilyMedicineDoctorsDto(){
        var allFamilyMedicineDoctors = await _unitOfWork.DoctorRepository.GetAllFamilyMedicineDoctors();
        
        _familyMedicineDoctorsDto.Clear();
        _familyMedicineDoctorsDto.AddRange(allFamilyMedicineDoctors);
    }
}