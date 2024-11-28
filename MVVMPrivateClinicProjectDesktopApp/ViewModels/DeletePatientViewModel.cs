using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class DeletePatientViewModel : ViewModelBase {
    public int PatientIdToDelete { get; set; }
    public string? PatientCode { get; set; }

    public ICommand DeletePatientCommand { get; set; }

    public DeletePatientViewModel(PatientStore patientStore){
        PatientIdToDelete = patientStore.PatientIdToDelete;
        DeletePatientCommand = new DeletePatientCommand(this, patientStore);
    }
}