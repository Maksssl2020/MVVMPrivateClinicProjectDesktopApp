using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PatientDataModalViewModel : EntityDetailsViewModelBase<PatientDto> {
    private readonly PatientDataModalNavigationStore _patientDataModalNavigationStore;

    public PatientDataModalNavigationViewModel PatientDataModalNavigationViewModel { get; }
    public ViewModelBase CurrentViewModel => _patientDataModalNavigationStore.CurrentViewModel!;
    
    private PatientDataModalViewModel(PatientDataModalNavigationStore patientDataModalNavigationStore, PatientDataModalNavigationViewModel patientDataModalNavigationViewModel, PatientStore patientStore)
        :base(new LoadEntityDetailsCommand<PatientDto, PatientDto>(patientStore)){
        _patientDataModalNavigationStore = patientDataModalNavigationStore;
        PatientDataModalNavigationViewModel = patientDataModalNavigationViewModel;
        
        _patientDataModalNavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    public static PatientDataModalViewModel LoadPatientDataModalViewModel(
        PatientDataModalNavigationStore patientDataModalNavigationStore,
        PatientDataModalNavigationViewModel patientDataModalNavigationViewModel, PatientStore patientStore){
        var patientDataModalViewModel = new PatientDataModalViewModel(patientDataModalNavigationStore,
            patientDataModalNavigationViewModel, patientStore);

        patientDataModalViewModel.LoadEntityCommand.Execute(patientDataModalViewModel);
        
        patientDataModalNavigationViewModel.ShowPatientDetailsViewCommand.Execute(null);

        return patientDataModalViewModel;
    }

    private void OnCurrentViewModelChanged(){
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}