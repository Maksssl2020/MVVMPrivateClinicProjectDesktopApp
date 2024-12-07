using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadDiseasesCommand(DiseasesViewModel viewModel, DiseaseStore diseaseStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await diseaseStore.LoadDiseases();
            viewModel.UpdateDiseases(diseaseStore.DiseasesDto);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}