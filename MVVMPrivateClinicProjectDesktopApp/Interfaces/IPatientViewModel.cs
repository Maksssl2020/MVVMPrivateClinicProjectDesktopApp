using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Interfaces;

public interface IPatientViewModel {
    public void UpdatePatients(IEnumerable<PatientDto> patients);
}