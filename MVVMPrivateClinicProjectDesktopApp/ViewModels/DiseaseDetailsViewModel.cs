using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class DiseaseDetailsViewModel : ViewModelBase {
    private DiseaseDetailsDto _diseaseDetails = null!;
    public DiseaseDetailsDto DiseaseDetails {
        get => _diseaseDetails;
        set {
            _diseaseDetails = value;
            OnPropertyChanged();
        }
    }

    private ICommand LoadDiseaseCommand { get; set; }

    private DiseaseDetailsViewModel(DiseaseStore diseaseStore){
        LoadDiseaseCommand = new LoadDiseaseCommand(this, diseaseStore);
    }

    public static DiseaseDetailsViewModel LoadDiseaseDetailsViewModel(DiseaseStore diseaseStore){
        var diseaseDetailsViewModel = new DiseaseDetailsViewModel(diseaseStore);

        diseaseDetailsViewModel.LoadDiseaseCommand.Execute(null);
        
        return diseaseDetailsViewModel;
    }
}