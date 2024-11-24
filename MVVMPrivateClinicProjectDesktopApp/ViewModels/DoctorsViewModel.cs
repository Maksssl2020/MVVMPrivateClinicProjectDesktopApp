using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using AutoMapper;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Models.Entities;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Doctor;
using MVVMPrivateClinicProjectDesktopApp.Repositories.DoctorSpecialization;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class DoctorsViewModel : ViewModelBase {
    private readonly IDoctorRepository _doctorRepository;

    private string _doctorsFilter = string.Empty;
    
    private ObservableCollection<DoctorDTO> Doctors { get; set; } = [];
    public ICollectionView DoctorsView { get; set; }
    private string _doctorSpecialization = string.Empty;
    
    public string DoctorsFilter {
        get => _doctorsFilter;
        set {
            _doctorsFilter = value;
            OnPropertyChanged();
            DoctorsView.Refresh();
        }
    }

    public string DoctorSpecialization {
        get => _doctorSpecialization;
        set {
            _doctorSpecialization = value;
            OnPropertyChanged();
        }
    }
    
    public DoctorsViewModel() {
        _doctorRepository = new DoctorRepository(MyMapper.Mapper ,new DoctorSpecializationRepository());
        
        LoadDoctorsAsync();
        
        DoctorsView = CollectionViewSource.GetDefaultView(Doctors);
        DoctorsView.Filter = FilterDoctors;
    }
    
    private async void LoadDoctorsAsync() {
        try {
            var doctors = await _doctorRepository.GetAllDoctors();

            foreach (var doctor in doctors) {
                Doctors.Add(doctor);
            }

            Console.WriteLine("Doctors Loaded!");
        }
        catch (Exception e) {
            Console.WriteLine("Something went wrong... " + e.Message);
        }
    }
    
    private bool FilterDoctors(object obj){
        if (obj is not DoctorDTO doctorDto) {
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
}