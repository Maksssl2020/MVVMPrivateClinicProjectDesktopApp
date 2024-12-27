using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Base;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.DoctorRepository;

public interface IDoctorRepository : IBaseRepository<DoctorDto> {
    Task<DoctorDto> SaveDoctorAsync(SaveDoctorRequest doctorRequest);
    Task<IEnumerable<DoctorDto>> GetAllDoctors();
    Task<IEnumerable<DoctorDto>> GetAllFamilyMedicineDoctors();
    Task<IEnumerable<DoctorDto>> GetMostPopularDoctors(int size);
    Task<DoctorDto?> GetDoctorByIdAsync(int doctorId);
    Task<DoctorDtoBase?> GetDoctorDetailsAsync(int doctorId);
    Task<int> CountDoctorsAsync();
}