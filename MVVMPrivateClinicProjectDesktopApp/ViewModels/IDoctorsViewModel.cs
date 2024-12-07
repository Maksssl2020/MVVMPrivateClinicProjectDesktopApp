using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public interface IDoctorsViewModel {
    public void UpdateDoctorsDto(IEnumerable<DoctorDto> doctorsDto);
}