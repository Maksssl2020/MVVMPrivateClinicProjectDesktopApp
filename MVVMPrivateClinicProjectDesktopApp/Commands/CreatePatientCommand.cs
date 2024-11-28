using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Address;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class CreatePatientCommand(AddNewPatientViewModel addNewPatientViewModel, PatientStore patientStore)
    : AsyncRelayCommand {
    private readonly IAddressRepository _addressRepository = new AddressRepository();
    
    public override async Task ExecuteAsync(object? parameter){
        var saveAddressRequest = new SaveAddressRequest {
            City = addNewPatientViewModel.City,
            PostalCode = addNewPatientViewModel.PostalCode,
            Street = addNewPatientViewModel.Street,
            BuildingNumber = addNewPatientViewModel.BuildingNumber,
            LocalNumber = addNewPatientViewModel.LocalNumber,
        };
        
        var savedAddress = await _addressRepository.SaveAddressAsync(saveAddressRequest);

        var savePatientRequest = new SavePatientRequest {
            FirstName = addNewPatientViewModel.FirstName,
            LastName = addNewPatientViewModel.LastName,
            PhoneNumber = addNewPatientViewModel.PhoneNumber,
            Email = addNewPatientViewModel.Email,
            AddressId = savedAddress.Id,
        };

        await patientStore.CreatePatient(savePatientRequest);
    }
}