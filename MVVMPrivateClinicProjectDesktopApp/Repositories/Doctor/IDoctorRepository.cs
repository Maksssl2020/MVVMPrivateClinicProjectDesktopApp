using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Doctor;

public interface IDoctorRepository {
    Task<IEnumerable<DoctorDto>> GetAllDoctors();
    Task<DoctorDto?> GetDoctorByIdAsync(int id);
}