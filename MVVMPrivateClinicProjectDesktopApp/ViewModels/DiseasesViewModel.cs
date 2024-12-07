using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Disease;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class DiseasesViewModel : ViewModelBase {
    private string _diseasesFilter = string.Empty;

    private readonly ObservableCollection<DiseaseDto> _diseases; 
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
    public ICommand ShowAddNewDiseaseModalCommand { get; set; }
    
    private DiseasesViewModel(DiseaseStore diseaseStore, ModalNavigationViewModel modalNavigationViewModel){
        _diseases = [];
        
        DiseasesView = CollectionViewSource.GetDefaultView(_diseases);
        DiseasesView.Filter = FilterDiseases;

        LoadDiseasesCommand = new LoadDiseasesCommand(this, diseaseStore);
        ShowAddNewDiseaseModalCommand = modalNavigationViewModel.ShowAddNewDiseaseModal;

        diseaseStore.DiseaseCreated += OnDiseaseCreated;
    }

    public static DiseasesViewModel LoadDiseasesViewModel(DiseaseStore diseaseStore, ModalNavigationViewModel modalNavigationViewModel){
        var diseasesViewModel = new DiseasesViewModel(diseaseStore, modalNavigationViewModel);
        
        diseasesViewModel.LoadDiseasesCommand.Execute(null);

        return diseasesViewModel;
    }

    public void UpdateDiseases(IEnumerable<DiseaseDto> diseases){
        _diseases.Clear();

        foreach (var disease in diseases) {
            _diseases.Add(disease);
        }
    }
    
    private void OnDiseaseCreated(DiseaseDto diseaseDto){
        _diseases.Add(diseaseDto);
    }
    
    private bool FilterDiseases(object obj){
        if (obj is not DiseaseDto disease) {
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