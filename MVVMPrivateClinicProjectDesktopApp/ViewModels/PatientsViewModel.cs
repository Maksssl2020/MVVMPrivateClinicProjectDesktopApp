using System.Collections.ObjectModel;
using MVVMPrivateClinicProjectDesktopApp.Entities;
using MVVMPrivateClinicProjectDesktopApp.Repositories;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PatientsViewModel : ViewModelBase {
    private readonly IPatientRepository _patientRepository;

    public ObservableCollection<Patient> Patients { get; set; } = [];
    
    public PatientsViewModel(){
        _patientRepository = new PatientRepository();
        LoadPatients();
    }

    private void LoadPatients(){
         var patients = _patientRepository.GetAllPatients();
         Patients = new ObservableCollection<Patient>(patients);
    }
}