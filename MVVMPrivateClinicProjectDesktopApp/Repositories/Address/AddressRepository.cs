using Microsoft.EntityFrameworkCore;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Address;

public class AddressRepository : RepositoryBase, IAddressRepository {
    public Models.Entities.Address? GetAddressByPatientId(int patientId){
        return DbContext.Addresses
            .FirstOrDefault(address => address.Patients
                .Any(patient => patient.Id == patientId));
    }
}