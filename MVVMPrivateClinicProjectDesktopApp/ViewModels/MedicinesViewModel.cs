using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Medicine;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class MedicinesViewModel : ViewModelBase {
    private readonly IUnitOfWork _unitOfWork;
    
    private string _medicineFilter = string.Empty;

    private ObservableCollection<Medicine> Medicines { get; set; } = [];
    public ICollectionView MedicinesView { get; set; }

    public string MedicinesFilter {
        get => _medicineFilter;
        set {
            _medicineFilter = value;
            OnPropertyChanged();
            MedicinesView.Refresh();
        }
    }

    public MedicinesViewModel(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;

        LoadMedicinesAsync();
        
        MedicinesView = CollectionViewSource.GetDefaultView(Medicines);
        MedicinesView.Filter = FilterMedicines;
    }
    
    private async void LoadMedicinesAsync() {
        try {
            var medicines = await _unitOfWork.MedicineRepository.GetAllMedicinesAsync();

            foreach (var medicine in medicines) {
                Medicines.Add(medicine);
            }
        }
        catch (Exception e) {
            Console.WriteLine("Something went wrong... " + e.Message);
        }
    }
    
    private bool FilterMedicines(object obj){
        if (obj is not Medicine medicine) {
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