using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class MedicinesViewModel : DisplayEntitiesViewModelBase<MedicineDto, MedicineDetailsDto> {
    public ICommand ShowAddNewMedicineModal { get; set; }
    public ICommand ShowMedicineDetailsModal { get; }

    private MedicinesViewModel(MedicineStore medicineStore, ModalNavigationViewModel modalNavigationViewModel)
        :base([SortingOptions.IdAscending, SortingOptions.IdDescending, SortingOptions.AlphabeticalAscending, SortingOptions.AlphabeticalDescending],
            medicineStore, 
            modalNavigationViewModel) {
        ShowAddNewMedicineModal = modalNavigationViewModel.ShowAddNewMedicineModal;
        ShowMedicineDetailsModal = modalNavigationViewModel.ShowMedicineDetailsModal;
    }

    public static MedicinesViewModel LoadMedicinesViewModel(MedicineStore medicineStore, ModalNavigationViewModel modalNavigationViewModel){
        var medicinesViewModel = new MedicinesViewModel(medicineStore, modalNavigationViewModel);
        
        medicinesViewModel.LoadEntitiesCommand.Execute(null);
        
        return medicinesViewModel;
    }

    protected override void UpdateEntities(IEnumerable<MedicineDto> entities){
        Entities.Clear();

        foreach (var entity in entities) {
            Entities.Add(entity);
        }
        
        SelectedSortingOption = SortingOptions.IdAscending;
    }

    protected override void SortEntities(){
        var nameOfProperty = SelectedSortingOption is SortingOptions.IdAscending or SortingOptions.IdDescending ? nameof(MedicineDto.Id) : nameof(MedicineDto.Name);
        ApplySortingOptions.ApplySortingWithOneProperty(EntitiesView, SelectedSortingOption, nameOfProperty);
    }

    protected override bool ApplyFilter(object obj){
        if (obj is not MedicineDto medicine) {
            return false;
        }

        if (string.IsNullOrWhiteSpace(Filter)) {
            return true;
        }
        
        var filter = Filter.Trim().ToLower();
        return medicine.Name.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
               medicine.Type.Contains(filter, StringComparison.CurrentCultureIgnoreCase);
    }
}