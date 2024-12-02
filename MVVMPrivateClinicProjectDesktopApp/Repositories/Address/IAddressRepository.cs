using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Address;

public interface IAddressRepository {
    Task<Models.Entities.Address?> SaveAddressAsync(SaveAddressRequest address);
    Task<Models.Entities.Address?> GetAddressByPatientId(int patientId);
}