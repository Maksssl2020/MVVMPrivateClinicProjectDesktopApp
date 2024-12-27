using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class DiseaseDetailsViewModel : EntityDetailsViewModelBase<DiseaseDetailsDto> {
   private DiseaseDetailsViewModel(DiseaseStore diseaseStore)
        :base(new LoadEntityDetailsCommand<DiseaseDto, DiseaseDetailsDto>(diseaseStore)){
    }

    public static DiseaseDetailsViewModel LoadDiseaseDetailsViewModel(DiseaseStore diseaseStore){
        var diseaseDetailsViewModel = new DiseaseDetailsViewModel(diseaseStore);

        diseaseDetailsViewModel.LoadEntityCommand.Execute(diseaseDetailsViewModel);
        
        return diseaseDetailsViewModel;
    }
}