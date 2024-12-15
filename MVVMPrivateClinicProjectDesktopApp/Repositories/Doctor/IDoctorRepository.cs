using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Doctor;

public interface IDoctorRepository {
    Task<DoctorDto> SaveDoctorAsync(SaveDoctorRequest doctorRequest);
    Task<IEnumerable<DoctorDto>> GetAllDoctors();
    Task<IEnumerable<DoctorDto>> GetAllFamilyMedicineDoctors();
    Task<IEnumerable<DoctorDto>> GetMostPopularDoctors(int size);
    Task<DoctorDto?> GetDoctorByIdAsync(int id);
    Task<DoctorDtoBase?> GetDoctorDetailsAsync(int doctorId);
    Task<int> CountDoctorsAsync();
}