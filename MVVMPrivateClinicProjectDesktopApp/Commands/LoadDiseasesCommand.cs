using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadDiseasesCommand(Action<IEnumerable<DiseaseDto>> updateDiseases, DiseaseStore diseaseStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await diseaseStore.LoadDiseases();
            updateDiseases.Invoke(diseaseStore.DiseasesDto);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}