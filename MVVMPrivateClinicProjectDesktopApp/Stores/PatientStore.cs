using MVVMPrivateClinicProjectDesktopApp.Models.Entities;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class PatientStore {
    
    public event Action<Patient>? PatientCreated;
    
    public void CreatePatient(Patient patient){
        PatientCreated?.Invoke(patient);
    }
}