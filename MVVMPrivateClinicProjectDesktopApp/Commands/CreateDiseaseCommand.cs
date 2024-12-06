using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class CreateDiseaseCommand(AddNewDiseaseViewModel viewModel, DiseaseStore diseaseStore) : AsyncRelayCommand {
    public override Task ExecuteAsync(object? parameter){
        throw new NotImplementedException();
    }
}