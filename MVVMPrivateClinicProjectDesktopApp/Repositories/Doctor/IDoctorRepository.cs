using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Doctor;

public interface IDoctorRepository {
    Task<IEnumerable<DoctorDTO>> GetAllDoctors();
}