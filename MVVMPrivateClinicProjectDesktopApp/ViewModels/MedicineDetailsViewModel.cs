using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class MedicineDetailsViewModel : EntityDetailsViewModelBase<MedicineDetailsDto> {
    private MedicineDetailsViewModel(MedicineStore medicineStore)
        :base(new LoadEntityDetailsCommand<MedicineDto, MedicineDetailsDto>(medicineStore)){
    }

    public static MedicineDetailsViewModel LoadMedicineDetailsViewModel(MedicineStore medicineStore){
        var medicineDetailsViewModel = new MedicineDetailsViewModel(medicineStore);
        
        medicineDetailsViewModel.LoadEntityCommand.Execute(medicineDetailsViewModel);
        
        return medicineDetailsViewModel;
    }
}