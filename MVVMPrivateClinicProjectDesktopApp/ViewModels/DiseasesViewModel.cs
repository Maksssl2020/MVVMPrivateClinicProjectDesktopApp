using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class DiseasesViewModel : DisplayEntitiesViewModelBase<DiseaseDto> {
    private ICommand LoadDiseasesCommand { get; set; }
    public ICommand ShowAddNewDiseaseModalCommand { get; set; }
    
    private DiseasesViewModel(DiseaseStore diseaseStore, ModalNavigationViewModel modalNavigationViewModel)
        :base([SortingOptions.IdAscending, SortingOptions.IdDescending, SortingOptions.AlphabeticalAscending, SortingOptions.AlphabeticalDescending]){
        LoadDiseasesCommand = new LoadDiseasesCommand(UpdateEntities, diseaseStore);
        ShowAddNewDiseaseModalCommand = modalNavigationViewModel.ShowAddNewDiseaseModal;

        diseaseStore.DiseaseCreated += OnDiseaseCreated;
    }

    public static DiseasesViewModel LoadDiseasesViewModel(DiseaseStore diseaseStore, ModalNavigationViewModel modalNavigationViewModel){
        var diseasesViewModel = new DiseasesViewModel(diseaseStore, modalNavigationViewModel);
        
        diseasesViewModel.LoadDiseasesCommand.Execute(null);

        return diseasesViewModel;
    }
    
    private void OnDiseaseCreated(DiseaseDto diseaseDto){
        Entities.Add(diseaseDto);
    }

    public override void UpdateEntities(IEnumerable<DiseaseDto> entities){
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