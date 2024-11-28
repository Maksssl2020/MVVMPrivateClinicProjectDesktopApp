using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Address;

public class AddressRepository : RepositoryBase, IAddressRepository {
    public async Task<Models.Entities.Address> SaveAddressAsync(SaveAddressRequest address) {

        var foundAddress = await AddressExists(address);
        
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
        
        await DbContext.Addresses.AddAsync(createdAddress);
        await DbContext.SaveChangesAsync();
        
        return createdAddress;
    }

    private async Task<Models.Entities.Address?> AddressExists(SaveAddressRequest address){
        return await DbContext.Addresses
            .FirstOrDefaultAsync(
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