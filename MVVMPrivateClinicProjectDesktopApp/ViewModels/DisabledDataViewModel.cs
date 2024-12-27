using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.IdentityModel.Tokens;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class DisabledDataViewModel : DisplayEntitiesViewModelBase<DisabledDataDto, DisabledDataDto> {
    private readonly DisabledDataStore _disabledDataStore;
    
    private string _chosenCategory = string.Empty;
    public string ChosenCategory {
        get => _chosenCategory;
        private set {
            _chosenCategory = value;
            OnPropertyChanged();
            _disabledDataStore.Category = _chosenCategory;
        }
    }
    
    public ICommand ChooseCategory { get; set; }
    private ICommand RestoreDisableDataCommand { get; set; }

    private DisabledDataViewModel(DisabledDataStore disabledDataStore) : 
        base([SortingOptions.IdAscending, SortingOptions.IdDescending, SortingOptions.DateAscending, SortingOptions.DateDescending],disabledDataStore, null!){
        _disabledDataStore = disabledDataStore;
        ChooseCategory = new RelayCommand<string>(SetCategory);
        RestoreDisableDataCommand = new RestoreDisabledDataCommand(disabledDataStore);
    }

    public static DisabledDataViewModel LoadDisabledDataViewModel(DisabledDataStore disabledDataStore){
        var disabledDataViewModel = new DisabledDataViewModel(disabledDataStore);
        
        disabledDataViewModel.SetCategory("Patients");
        
        return disabledDataViewModel;
    }

    public void RestoreDisableData(int dataId){
        RestoreDisableDataCommand.Execute(dataId);
    }
    
    private async void SetCategory(string? category){
        if (string.IsNullOrWhiteSpace(category)) {
            return;
        }

        ChosenCategory = category;
        await _disabledDataStore.ReloadData();
        UpdateEntities(_disabledDataStore.EntitiesCollection);
    }

    protected override void UpdateEntities(IEnumerable<DisabledDataDto> entities){
        Entities.Clear();
        
        foreach (var entity in entities) {
            Entities.Add(entity);
        }

        SelectedSortingOption = SortingOptions.IdAscending;
    }

    protected override void SortEntities(){
        ApplySortingOptions.ApplySortingWithOneProperty(EntitiesView, SelectedSortingOption,
            SelectedSortingOption is SortingOptions.IdAscending or SortingOptions.IdDescending
                ? nameof(DisabledDataDto.Id)
                : nameof(DisabledDataDto.DisabledDate));
    }

    protected override bool ApplyFilter(object obj){
        if (obj is not DisabledDataDto disabledDataDto) {
            return false;
        }

        if (Filter.IsNullOrEmpty()) {
            return true;
        }
        
        var filter = Filter.ToLower().Trim();
        
        return disabledDataDto.Id.ToString().Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
               disabledDataDto.DisabledDataType.ToString().Contains(filter, StringComparison.CurrentCultureIgnoreCase);
    }
}