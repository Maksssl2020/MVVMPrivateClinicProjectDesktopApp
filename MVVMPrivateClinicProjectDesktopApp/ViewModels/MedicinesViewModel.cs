using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class MedicinesViewModel : ViewModelBase, IMedicinesViewModel {
    private string _medicineFilter = string.Empty;

    private readonly ObservableCollection<MedicineDto> _medicines = [];
    public ICollectionView MedicinesView { get; set; }

    public string MedicinesFilter {
        get => _medicineFilter;
        set {
            _medicineFilter = value;
            OnPropertyChanged();
            MedicinesView.Refresh();
        }
    }

    private ICommand LoadMedicinesCommand { get; set; }
    public ICommand ShowAddNewMedicineModal { get; set; }

    private MedicinesViewModel(MedicineStore medicineStore, ModalNavigationViewModel modalNavigationViewModel){
        MedicinesView = CollectionViewSource.GetDefaultView(_medicines);
        MedicinesView.Filter = FilterMedicines;

        LoadMedicinesCommand = new LoadMedicinesDtoCommand(this, medicineStore);
        ShowAddNewMedicineModal = modalNavigationViewModel.ShowAddNewMedicineModal;
        medicineStore.MedicineCreated += OnMedicineCreated;
    }

    public static MedicinesViewModel LoadMedicinesViewModel(MedicineStore medicineStore, ModalNavigationViewModel modalNavigationViewModel){
        var medicinesViewModel = new MedicinesViewModel(medicineStore, modalNavigationViewModel);
        
        medicinesViewModel.LoadMedicinesCommand.Execute(null);
        
        return medicinesViewModel;
    }

    private void OnMedicineCreated(MedicineDto medicineDto){
        _medicines.Add(medicineDto);
    }
    
    public void UpdateMedicines(IEnumerable<MedicineDto> medicinesDto){
        _medicines.Clear();

        foreach (var medicineDto in medicinesDto) {
            _medicines.Add(medicineDto);
        }
    }
    
    private bool FilterMedicines(object obj){
        if (obj is not MedicineDto medicine) {
            return false;
        }

        if (string.IsNullOrWhiteSpace(MedicinesFilter)) {
            return true;
        }
        
        var filter = MedicinesFilter.Trim().ToLower();
        return medicine.Name.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
               medicine.Type.Contains(filter, StringComparison.CurrentCultureIgnoreCase);
    }
}