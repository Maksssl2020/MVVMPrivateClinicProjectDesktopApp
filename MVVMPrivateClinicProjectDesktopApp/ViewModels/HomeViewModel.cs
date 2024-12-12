using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class HomeViewModel : ViewModelBase {
   
    private readonly ObservableCollection<DoctorDto> _mostPopularDoctors;
    public ICollectionView MostPopularDoctorsView { get; set; }

    private ICommand LoadMostPopularDoctorsCommand { get; set; }
    
    private HomeViewModel(DoctorStore doctorStore){
        _mostPopularDoctors = [];
        
        MostPopularDoctorsView = CollectionViewSource.GetDefaultView(_mostPopularDoctors);
        
        LoadMostPopularDoctorsCommand = new LoadMostPopularDoctorsCommand(this, doctorStore);
    }

    public static HomeViewModel LoadHomeViewModel(DoctorStore doctorStore){
        var homeViewModel = new HomeViewModel(doctorStore);
        
        homeViewModel.LoadMostPopularDoctorsCommand.Execute(null);
        
        return homeViewModel;
    }
    
    public void UpdateMostPopularDoctors(IEnumerable<DoctorDto> mostPopularDoctorsDto){
        _mostPopularDoctors.Clear();

        foreach (var doctorDto in mostPopularDoctorsDto) {
            _mostPopularDoctors.Add(doctorDto);
        }
    }
}
    
