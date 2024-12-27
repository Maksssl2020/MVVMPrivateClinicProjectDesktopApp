using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class CreateDiagnosisCommand(AddNewDiagnosisViewModel viewModel, DiagnosisStore diagnosisStore, Action resetForm): AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            var saveDiagnosisRequest = new SaveDiagnosisRequest {
                Description = viewModel.DiagnosisDescription,
                DoctorId = viewModel.SelectedDoctor.Id,
                DiseaseId = viewModel.SelectedDisease.Id,
                PatientId = viewModel.SelectedPatientId
            };
            
            await diagnosisStore.CreateEntity(saveDiagnosisRequest);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}