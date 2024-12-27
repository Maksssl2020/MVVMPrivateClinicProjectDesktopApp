using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class PrescriptionDetailsViewModel(PrescriptionStore prescriptionStore)
    : EntityDetailsViewModelBase<PrescriptionDetailsDto>(
        new LoadEntityDetailsCommand<PrescriptionDto, PrescriptionDetailsDto>(prescriptionStore)) {
    
    public static PrescriptionDetailsViewModel LoadPrescriptionDetailsViewModel(PrescriptionStore prescriptionStore){
        var prescriptionDetailsViewModel = new PrescriptionDetailsViewModel(prescriptionStore);
        
        prescriptionDetailsViewModel.LoadEntityCommand.Execute(prescriptionDetailsViewModel);

        return prescriptionDetailsViewModel;
    }

}