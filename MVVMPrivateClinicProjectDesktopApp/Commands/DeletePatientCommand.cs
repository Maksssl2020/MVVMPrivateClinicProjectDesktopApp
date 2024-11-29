using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class DeletePatientCommand(DeletePatientViewModel deletePatientViewModel, PatientStore patientStore, Action closeModalCation) : RelayCommand {
    
    public override void Execute(object? parameter){
        var patientIdToDelete = deletePatientViewModel.PatientIdToDelete;
        patientStore.DeletePatient(patientIdToDelete);
        closeModalCation.Invoke();
    }
}