using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.DoctorSpecialization;

public interface IDoctorSpecializationRepository {
    Task<DoctorSpecializationDto> SaveDoctorSpecializationAsync(string doctorSpecializationName);
    Task<IEnumerable<DoctorSpecializationDto>> GetAllDoctorSpecializations();
    Task<DoctorSpecializationDto?> GetDoctorSpecializationById(int id);
    Task<bool> IsDoctorSpecializationExist(string doctorSpecializationName);
    Task<int?> GetDoctorSpecializationId(string doctorSpecializationName);
}