using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Base;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.DoctorSpecializationRepository;

public interface IDoctorSpecializationRepository {
    Task<DoctorSpecializationDto> SaveDoctorSpecializationAsync(string doctorSpecializationName);
    Task<IEnumerable<DoctorSpecializationDto>> GetAllDoctorSpecializations();
    Task<bool> IsDoctorSpecializationExists(string doctorSpecializationName);
    Task<int?> GetDoctorSpecializationId(string doctorSpecializationName);
}