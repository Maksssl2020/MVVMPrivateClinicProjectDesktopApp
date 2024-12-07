using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class CreateDoctorCommand(AddNewDoctorViewModel viewModel, DoctorStore doctorStore, DoctorSpecializationStore doctorSpecializationStore, Action resetForm) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        var doctorSpecializationId = await doctorSpecializationStore.GetDoctorSpecializationId(viewModel.DoctorSpecialization);

        var saveDoctorRequest = new SaveDoctorRequest {
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            PhoneNumber = viewModel.PhoneNumber,
            DoctorSpecializationId = doctorSpecializationId,
        };
        
        if (doctorSpecializationId > 0) {
            await doctorStore.CreateDoctor(saveDoctorRequest);
            resetForm.Invoke();
        }
    }
}