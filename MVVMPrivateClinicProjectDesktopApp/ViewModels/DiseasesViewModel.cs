using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Disease;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class DiseasesViewModel : ViewModelBase {
    private readonly DiseaseStore _diseaseStore;
    
    private string _diseasesFilter = string.Empty;

    private ObservableCollection<Disease> _diseases; 
    public ICollectionView DiseasesView  { get; set; }

    public string DiseasesFilter {
        get => _diseasesFilter;
        set {
            _diseasesFilter  = value;
            OnPropertyChanged();
            DiseasesView.Refresh();
        }
    }

    public ICommand LoadDiseasesCommand { get; set; }
    
    private DiseasesViewModel(DiseaseStore diseaseStore){
        _diseaseStore = diseaseStore;
        _diseases = [];
        
        DiseasesView = CollectionViewSource.GetDefaultView(_diseases);
        DiseasesView.Filter = FilterDiseases;

        LoadDiseasesCommand = new LoadDiseasesCommand(this, _diseaseStore);
    }

    public static DiseasesViewModel LoadDiseasesViewModel(DiseaseStore diseaseStore){
        var diseasesViewModel = new DiseasesViewModel(diseaseStore);
        
        diseasesViewModel.LoadDiseasesCommand.Execute(null);

        return diseasesViewModel;
    }

    public void UpdateDiseases(IEnumerable<Disease> diseases){
        _diseases.Clear();

        foreach (var disease in diseases) {
            _diseases.Add(disease);
        }
    }
    
    private bool FilterDiseases(object obj){
        if (obj is not Disease disease) {
            return false;
        }

        if (string.IsNullOrWhiteSpace(DiseasesFilter)) {
            return true;
        }

        var filter = DiseasesFilter.Trim().ToLower();
        return disease.Name.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
               disease.DiseaseCode.Contains(filter, StringComparison.InvariantCultureIgnoreCase);
    }
}