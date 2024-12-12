using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PatientDataModalViewModel : ViewModelBase {
    private readonly PatientDataModalNavigationStore _patientDataModalNavigationStore;

    public PatientDataModalNavigationViewModel PatientDataModalNavigationViewModel { get; }
    public ViewModelBase CurrentViewModel => _patientDataModalNavigationStore.CurrentViewModel!;

    private PatientDto? _selectedPatient;
    public PatientDto? SelectedPatient { 
        get => _selectedPatient;
        set {
            _selectedPatient = value;
            OnPropertyChanged();
        }
    }

    private ICommand LoadPatientCommand {get; set;}
    
    private PatientDataModalViewModel(PatientDataModalNavigationStore patientDataModalNavigationStore, PatientDataModalNavigationViewModel patientDataModalNavigationViewModel, PatientStore patientStore){
        _patientDataModalNavigationStore = patientDataModalNavigationStore;
        PatientDataModalNavigationViewModel = patientDataModalNavigationViewModel;

        LoadPatientCommand = new LoadPatientCommand(this, patientStore);
        
        _patientDataModalNavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    public static PatientDataModalViewModel LoadPatientDataModalViewModel(
        PatientDataModalNavigationStore patientDataModalNavigationStore,
        PatientDataModalNavigationViewModel patientDataModalNavigationViewModel, PatientStore patientStore){
        var patientDataModalViewModel = new PatientDataModalViewModel(patientDataModalNavigationStore, patientDataModalNavigationViewModel, patientStore);
        
        patientDataModalViewModel.LoadPatientCommand.Execute(null);
        patientDataModalNavigationViewModel.ShowPatientDetailsViewCommand.Execute(null);
        
        if (patientDataModalViewModel.SelectedPatient != null)
            patientDataModalNavigationStore.SelectedPatientId = patientDataModalViewModel.SelectedPatient.Id;

        return patientDataModalViewModel;
    }
    
    private void OnCurrentViewModelChanged(){
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}