using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PatientDataModalViewModel : ViewModelBase {
    private readonly PatientDataModalNavigationStore _patientDataModalNavigationStore;
    private readonly PatientStore _patientStore;
    public PatientDataModalNavigationViewModel PatientDataModalNavigationViewModel { get; }
    public ViewModelBase CurrentViewModel => _patientDataModalNavigationStore.CurrentViewModel!;

    private Patient? _selectedPatient;
    public Patient? SelectedPatient { 
        get => _selectedPatient;
        set {
            _selectedPatient = value;
            OnPropertyChanged();
        }
    }

    private readonly ICommand LoadPatientCommand;
    
    private PatientDataModalViewModel(PatientDataModalNavigationStore patientDataModalNavigationStore, PatientDataModalNavigationViewModel patientDataModalNavigationViewModel, PatientStore patientStore){
        _patientDataModalNavigationStore = patientDataModalNavigationStore;
        PatientDataModalNavigationViewModel = patientDataModalNavigationViewModel;
        _patientStore = patientStore;

        LoadPatientCommand = new LoadPatientCommand(this, _patientStore);
        
        _patientDataModalNavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    public static PatientDataModalViewModel LoadPatientDataModalViewModel(
        PatientDataModalNavigationStore patientDataModalNavigationStore,
        PatientDataModalNavigationViewModel patientDataModalNavigationViewModel, PatientStore patientStore){
        var patientDataModalViewModel = new PatientDataModalViewModel(patientDataModalNavigationStore, patientDataModalNavigationViewModel, patientStore);
        
        patientDataModalViewModel.LoadPatientCommand.Execute(null);
        patientDataModalNavigationViewModel.ShowPatientDetailsViewCommand.Execute(null);
        
        return patientDataModalViewModel;
    }
    
    private void OnCurrentViewModelChanged(){
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}