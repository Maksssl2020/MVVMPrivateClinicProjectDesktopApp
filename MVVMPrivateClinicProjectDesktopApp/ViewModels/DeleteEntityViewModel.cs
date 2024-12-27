using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class DeleteEntityViewModel : ViewModelBase {
    private string? _entityCode;
    public string? EntityCode {
        get => _entityCode;
        set {
            _entityCode = value;
            OnPropertyChanged();
        }
    }
    
    private string _entityName = string.Empty;
    public string EntityTypeName {
        get => _entityName;
        set {
            _entityName = value;
            OnPropertyChanged();
        }
    }

    public ICommand DeleteEntityCommand { get; set; } = null!;
    private Action? _closeModalAfterDelete;

    private IEntityStore _entityStore = null!;
    public IEntityStore EntityStore {
        get => _entityStore;
        set {
            _entityStore = value;
            LoadViewModel();
        }
    }

    private void LoadViewModel(){
        EntityCode = EntityStore.EntityCode ?? EntityStore.EntityIdToDelete.ToString();
        EntityTypeName = GetEntityTypeName(EntityStore);
        DeleteEntityCommand = new DeleteEntityCommand(EntityStore.EntityIdToDelete, EntityStore.DeleteEntity, CloseModal);
    }
    
    private static string GetEntityTypeName(IEntityStore entityStore) => entityStore switch {
        PatientStore => "Patient",
        ReferralTestStore => "Referral Test",
        ReferralStore => "Referral",
        MedicineStore => "Medicine",
        DoctorStore => "Doctor",
        DiseaseStore => "Disease",
        PricingStore => "Pricing",
        PrescriptionStore => "Prescription",
        PatientNoteStore => "Patient Note",
        DoctorSpecializationStore => "Doctor Specialization",
        DiagnosisStore => "Diagnosis",
        _ => "Unknown"
    };
    
    public void InitializeCloseModalEvent(Action closeModal){
        _closeModalAfterDelete = closeModal;
    }
    
    private void CloseModal(){
        _closeModalAfterDelete?.Invoke();
    }
}