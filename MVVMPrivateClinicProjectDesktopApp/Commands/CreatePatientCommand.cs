using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Address;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class CreatePatientCommand(AddNewPatientViewModel addNewPatientViewModel, PatientStore patientStore, Action resetForm)
    : AsyncRelayCommand {
    
    public override async Task ExecuteAsync(object? parameter){
        var saveAddressRequest = new SaveAddressRequest {
            City = addNewPatientViewModel.City,
            PostalCode = addNewPatientViewModel.PostalCode,
            Street = addNewPatientViewModel.Street,
            BuildingNumber = addNewPatientViewModel.BuildingNumber,
            LocalNumber = addNewPatientViewModel.LocalNumber,
        };

        var savedAddress = await patientStore.SavePatientAddress(saveAddressRequest);

        var patientEmail = addNewPatientViewModel.Email != "" ? addNewPatientViewModel.Email : null;
        
        var savePatientRequest = new SavePatientRequest {
            FirstName = addNewPatientViewModel.FirstName,
            LastName = addNewPatientViewModel.LastName,
            PhoneNumber = addNewPatientViewModel.PhoneNumber,
            Email = patientEmail,
            AddressId = savedAddress!.Id
        };

        await patientStore.CreatePatient(savePatientRequest);
        
        resetForm.Invoke();
    }
}