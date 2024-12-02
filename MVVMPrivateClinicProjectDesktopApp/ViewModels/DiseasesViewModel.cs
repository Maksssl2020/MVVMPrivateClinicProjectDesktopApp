using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Disease;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class DiseasesViewModel : ViewModelBase {
    private readonly IUnitOfWork _unitOfWork;
    
    private string _diseasesFilter = string.Empty;

    private ObservableCollection<Disease> Diseases { get; set; } = [];
    public ICollectionView DiseasesView  { get; set; }

    public string DiseasesFilter {
        get => _diseasesFilter;
        set {
            _diseasesFilter  = value;
            OnPropertyChanged();
            DiseasesView.Refresh();
        }
    }

    public DiseasesViewModel(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;
        
        LoadDiseasesAsync();
        DiseasesView = CollectionViewSource.GetDefaultView(Diseases);
        DiseasesView.Filter = FilterDiseases;
    }

    private async void LoadDiseasesAsync(){
        try {
            var diseases = await _unitOfWork.DiseaseRepository.GetAllDiseasesAsync();

            foreach (var disease in diseases) {
                Diseases.Add(disease);
            }
        }
        catch (Exception e) {
            Console.WriteLine("Something went wrong... " + e.Message);
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