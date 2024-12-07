using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadDiseasesCommand(IDiseasesViewModel viewModel, DiseaseStore diseaseStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await diseaseStore.LoadDiseases();
            viewModel.UpdateDiseasesDto(diseaseStore.DiseasesDto);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}