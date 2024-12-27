using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.AddressRepository;

public interface IAddressRepository {
    Task<Address?> SaveAddressAsync(SaveAddressRequest address);
    Task<Address?> GetAddressByPatientId(int patientId);
}