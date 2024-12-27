using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.AddressRepository;

public class AddressRepository(DbContextFactory dbContextFactory) : IAddressRepository {
    public async Task<Address?> SaveAddressAsync(SaveAddressRequest address) {
        await using var context = dbContextFactory.CreateDbContext();
        
        var foundAddress = await AddressExists(context, address);
        
        if (foundAddress != null) {
            return foundAddress;
        }

        var createdAddress = new Address {
            City = address.City,
            Street = address.Street,
            PostalCode = address.PostalCode,
            BuildingNumber = address.BuildingNumber,
            LocalNumber = address?.LocalNumber,
        };
        
        await context.Addresses.AddAsync(createdAddress);
        await context.SaveChangesAsync();
        
        return createdAddress;
    }

    private async Task<Address?> AddressExists(PrivateClinicContext context, SaveAddressRequest address){
        return await context.Addresses
            .FirstOrDefaultAsync(
                a =>
                    a.City == address.City &&
                    a.PostalCode == address.PostalCode &&
                    a.Street == address.Street &&
                    a.BuildingNumber == address.BuildingNumber &&
                    a.LocalNumber == address.LocalNumber
            );
    }
    
    public async Task<Address?> GetAddressByPatientId(int patientId){
        await using var context = dbContextFactory.CreateDbContext();
        return await context.Addresses
            .Include(address => address.Patients)
            .FirstOrDefaultAsync(address => address.Patients.Any(p => p.Id == patientId));
    }
}