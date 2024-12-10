using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class DoctorsViewModel : ViewModelBase, IDoctorsViewModel {
    private ObservableCollection<DoctorDto> _doctors { get; set; }
    public ICollectionView DoctorsView { get; set; }
    
    private string _doctorsFilter = string.Empty;
    
    public string DoctorsFilter {
        get => _doctorsFilter;
        set {
            _doctorsFilter = value;
            OnPropertyChanged();
            DoctorsView.Refresh();
        }
    }

    private string _doctorSpecialization = string.Empty;
    
    public string DoctorSpecialization {
        get => _doctorSpecialization;
        set {
            _doctorSpecialization = value;
            OnPropertyChanged();
        }
    }

    private ICommand LoadDoctorsCommand { get; set; }
    public ICommand ShowAddNewDoctorViewCommand { get; set; }
    
    private DoctorsViewModel(DoctorStore doctorStore, ModalNavigationViewModel modalNavigationViewModel) {
        _doctors = [];
        
        DoctorsView = CollectionViewSource.GetDefaultView(_doctors);
        DoctorsView.Filter = FilterDoctors;

        LoadDoctorsCommand = new LoadDoctorsCommand(this, doctorStore);
        ShowAddNewDoctorViewCommand = modalNavigationViewModel.ShowAddNewDoctorModal;
        doctorStore.DoctorCreated += OnDoctorCreated;
    }

    public static DoctorsViewModel LoadDoctorsViewModel(DoctorStore doctorStore, ModalNavigationViewModel modalNavigationViewModel){
        var doctorsViewModel = new DoctorsViewModel(doctorStore, modalNavigationViewModel);
        
        doctorsViewModel.LoadDoctorsCommand.Execute(null);
        
        return doctorsViewModel;
    }

    private void OnDoctorCreated(DoctorDto doctor){
        _doctors.Add(doctor);
    }
    
    private bool FilterDoctors(object obj){
        if (obj is not DoctorDto doctorDto) {
            return false;
        }

        if (string.IsNullOrWhiteSpace(DoctorsFilter)) {
            return true;
        }
        
        var filter = DoctorsFilter.Trim().ToLower();
        return doctorDto.DoctorCode!.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
               doctorDto.FirstName.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
               doctorDto.LastName.Contains(filter, StringComparison.CurrentCultureIgnoreCase);
    }

    public void UpdateDoctorsDto(IEnumerable<DoctorDto> allDoctors){
        _doctors.Clear();

        foreach (var doctorDto in allDoctors) {
            _doctors.Add(doctorDto);
        }
    }
}