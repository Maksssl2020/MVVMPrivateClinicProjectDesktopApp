using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class CreatePatientCommand(AddNewPatientViewModel addNewPatientViewModel, PatientStore patientStore)
    : RelayCommand {
    
    private readonly AddNewPatientViewModel _addNewPatientViewModel = addNewPatientViewModel;
    private readonly PatientStore _patientStore = patientStore;

    public override void Execute(object? parameter){
        
    }
}