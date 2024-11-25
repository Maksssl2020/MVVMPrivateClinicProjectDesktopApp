using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Address;

public interface IAddressRepository {
    Models.Entities.Address SaveAddress(SaveAddressRequest address);
    Models.Entities.Address? GetAddressByPatientId(int patientId);
}