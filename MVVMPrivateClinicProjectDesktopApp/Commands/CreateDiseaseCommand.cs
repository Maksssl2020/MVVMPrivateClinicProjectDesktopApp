using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class CreateDiseaseCommand(AddNewDiseaseViewModel viewModel, DiseaseStore diseaseStore, Action resetForm) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        var saveDiseaseRequest = new SaveDiseaseRequest {
            DiseaseName = viewModel.DiseaseName
        };

        await diseaseStore.CreateDisease(saveDiseaseRequest);
        resetForm.Invoke();
    }
}