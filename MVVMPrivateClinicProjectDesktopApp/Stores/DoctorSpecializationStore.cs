using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class DoctorSpecializationStore {
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly List<DoctorSpecializationDto> _doctorSpecializations;
    private readonly Lazy<Task> _initializeLazy;

    public IEnumerable<DoctorSpecializationDto> DoctorSpecializations => _doctorSpecializations;

    public DoctorSpecializationStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;

        _doctorSpecializations = [];
        _initializeLazy = new Lazy<Task>(InitializeDoctorSpecializations);
    }

    public async Task LoadDoctorSpecializations(){
        await _initializeLazy.Value;
    }

    public async Task<int> GetDoctorSpecializationId(string doctorSpecializationName){
        if (await _unitOfWork.DoctorSpecializationRepository.IsDoctorSpecializationExist(doctorSpecializationName)) {
            var foundDoctorSpecializationId = await _unitOfWork.DoctorSpecializationRepository.GetDoctorSpecializationId(doctorSpecializationName);
            
            return foundDoctorSpecializationId ?? -1;
        }

        var doctorSpecialization = CreateDoctorSpecialization(doctorSpecializationName);
        return doctorSpecialization.Id;
    }

    private async Task<DoctorSpecializationDto> CreateDoctorSpecialization(string doctorSpecializationName){
        var savedDoctorSpecialization = await _unitOfWork.DoctorSpecializationRepository.SaveDoctorSpecializationAsync(
            doctorSpecializationName);
        
        _doctorSpecializations.Add(savedDoctorSpecialization);
        return savedDoctorSpecialization;
    }
    
    private async Task InitializeDoctorSpecializations(){
        var loadedDoctorSpecializations = await _unitOfWork.DoctorSpecializationRepository.GetAllDoctorSpecializations();
        
        _doctorSpecializations.Clear();
        _doctorSpecializations.AddRange(loadedDoctorSpecializations);
    }
}