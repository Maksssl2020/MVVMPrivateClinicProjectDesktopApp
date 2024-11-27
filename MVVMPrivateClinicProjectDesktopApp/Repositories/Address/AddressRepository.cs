using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Address;

public class AddressRepository : RepositoryBase, IAddressRepository {
    public Models.Entities.Address SaveAddress(SaveAddressRequest address) {

        var foundAddress = AddressExists(address);
        
        if (foundAddress != null) {
            return foundAddress;
        }

        var createdAddress = new Models.Entities.Address {
            City = address.City,
            Street = address.Street,
            PostalCode = address.PostalCode,
            BuildingNumber = address.BuildingNumber,
            LocalNumber = address?.LocalNumber,
        };
        
        DbContext.Addresses.Add(createdAddress);
        DbContext.SaveChanges();
        
        return createdAddress;
    }

    private Models.Entities.Address? AddressExists(SaveAddressRequest address){
        return DbContext.Addresses
            .FirstOrDefault(
                a =>
                    a.City == address.City &&
                    a.PostalCode == address.PostalCode &&
                    a.Street == address.Street &&
                    a.BuildingNumber == address.BuildingNumber &&
                    a.LocalNumber == address.LocalNumber
            );
    }
    
    public Models.Entities.Address? GetAddressByPatientId(int patientId){
        return DbContext.Addresses
            .FirstOrDefault(address => address.Patients
                .Any(patient => patient.Id == patientId));
    }
}