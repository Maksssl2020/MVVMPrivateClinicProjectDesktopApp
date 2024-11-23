namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Address;

public interface IAddressRepository {
    Models.Entities.Address? GetAddressByPatientId(int patientId);
}