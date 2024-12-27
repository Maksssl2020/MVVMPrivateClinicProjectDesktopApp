using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class DiseasesViewModel : DisplayEntitiesViewModelBase<DiseaseDto, DiseaseDetailsDto> {
    public ICommand ShowAddNewDiseaseModalCommand { get; set; }
    public ICommand ShowDiseaseDetailsModalCommand { get; set; }

    private DiseasesViewModel(DiseaseStore diseaseStore, ModalNavigationViewModel modalNavigationViewModel)
        : base(
            [
                SortingOptions.IdAscending, SortingOptions.IdDescending, SortingOptions.AlphabeticalAscending,
                SortingOptions.AlphabeticalDescending
            ],
            diseaseStore,
            modalNavigationViewModel){

        ShowAddNewDiseaseModalCommand = modalNavigationViewModel.ShowAddNewDiseaseModal;
        ShowDiseaseDetailsModalCommand = modalNavigationViewModel.ShowDiseaseDetailsModal;
    }

    public static DiseasesViewModel LoadDiseasesViewModel(DiseaseStore diseaseStore, ModalNavigationViewModel modalNavigationViewModel){
        var diseasesViewModel = new DiseasesViewModel(diseaseStore, modalNavigationViewModel);
        
        diseasesViewModel.LoadEntitiesCommand.Execute(null);

        return diseasesViewModel;
    }

    protected override void UpdateEntities(IEnumerable<DiseaseDto> entities){
        Entities.Clear();

        foreach (var entity in entities) {
            Entities.Add(entity);
        }
        
        SelectedSortingOption = SortingOptions.IdAscending;
    }

    protected override void SortEntities(){
        ApplySortingOptions.ApplySortingWithOneProperty(EntitiesView, SelectedSortingOption,
            SelectedSortingOption is SortingOptions.IdAscending or SortingOptions.IdDescending
                ? nameof(DiseaseDto.Id)
                : nameof(DiseaseDto.Name));
    }

    protected override bool ApplyFilter(object obj){
        if (obj is not DiseaseDto disease) {
            return false;
        }

        if (string.IsNullOrWhiteSpace(Filter)) {
            return true;
        }

        var filter = Filter.Trim().ToLower();
        return disease.Name.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
               disease.DiseaseCode.Contains(filter, StringComparison.InvariantCultureIgnoreCase);
    }
}