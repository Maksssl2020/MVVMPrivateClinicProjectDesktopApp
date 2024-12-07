using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Interfaces;

public interface IDoctorsViewModel {
    public void UpdateDoctorsDto(IEnumerable<DoctorDto> doctorsDto);
}