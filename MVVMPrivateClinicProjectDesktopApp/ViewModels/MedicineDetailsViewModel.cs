using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class MedicineDetailsViewModel : ViewModelBase {
    private MedicineDetailsDto _medicineDetails = null!;
    public MedicineDetailsDto MedicineDetails {
        get => _medicineDetails;
        set {
            _medicineDetails = value;
            OnPropertyChanged();
        }
    }

    private ICommand LoadMedicineCommand { get; set; }

    private MedicineDetailsViewModel(MedicineStore medicineStore){
        LoadMedicineCommand = new LoadMedicineCommand(this, medicineStore);
    }

    public static MedicineDetailsViewModel LoadMedicineDetailsViewModel(MedicineStore medicineStore){
        var medicineDetailsViewModel = new MedicineDetailsViewModel(medicineStore);
        
        medicineDetailsViewModel.LoadMedicineCommand.Execute(null);
        
        return medicineDetailsViewModel;
    }
}