using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadDiseaseCommand(DiseaseDetailsViewModel viewModel, DiseaseStore diseaseStore): AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await diseaseStore.LoadDisease();
            viewModel.DiseaseDetails = diseaseStore.DiseaseDetails;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}