using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Doctor;

public interface IDoctorRepository {
    Task<DoctorDto> SaveDoctorAsync(SaveDoctorRequest doctorRequest);
    Task<IEnumerable<DoctorDto>> GetAllDoctors();
    Task<IEnumerable<DoctorDto>> GetAllFamilyMedicineDoctors();
    Task<DoctorDto?> GetDoctorByIdAsync(int id);
}