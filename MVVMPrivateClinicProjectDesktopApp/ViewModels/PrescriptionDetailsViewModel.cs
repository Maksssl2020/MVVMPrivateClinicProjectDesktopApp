using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PrescriptionDetailsViewModel : ViewModelBase {
    
    private PrescriptionDetailsDto _prescriptionDetails = null!;
    public PrescriptionDetailsDto Prescription {
        get => _prescriptionDetails;
        set {
            _prescriptionDetails = value;
            OnPropertyChanged();
        }
    }

    private ICommand LoadPrescriptionCommand { get; set; }
    
    private PrescriptionDetailsViewModel(PrescriptionStore prescriptionStore){
        LoadPrescriptionCommand = new LoadPrescriptionCommand(this, prescriptionStore);
    }

    public static PrescriptionDetailsViewModel LoadPrescriptionDetailsViewModel(PrescriptionStore prescriptionStore){
        var prescriptionDetailsViewModel = new PrescriptionDetailsViewModel(prescriptionStore);
        
        prescriptionDetailsViewModel.LoadPrescriptionCommand.Execute(null);

        Console.WriteLine(prescriptionStore.SelectedPrescriptionId);
        
        return prescriptionDetailsViewModel;
    }

}