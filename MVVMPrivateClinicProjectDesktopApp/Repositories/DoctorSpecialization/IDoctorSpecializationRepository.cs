namespace MVVMPrivateClinicProjectDesktopApp.Repositories.DoctorSpecialization;

public interface IDoctorSpecializationRepository {
    Task<IEnumerable<Models.Entities.DoctorSpecialization>> GetAllDoctorSpecializations();
    Task<Models.Entities.DoctorSpecialization?> GetDoctorSpecializationById(int id);
}