using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using AutoMapper;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Doctor;
using MVVMPrivateClinicProjectDesktopApp.Repositories.DoctorSpecialization;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class DoctorsViewModel : ViewModelBase {
    private readonly IUnitOfWork _unitOfWork;

    private string _doctorsFilter = string.Empty;
    
    private ObservableCollection<DoctorDto> Doctors { get; set; } = [];
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
    
    public DoctorsViewModel(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
        
        LoadDoctorsAsync();
        
        DoctorsView = CollectionViewSource.GetDefaultView(Doctors);
        DoctorsView.Filter = FilterDoctors;
    }
    
    private async void LoadDoctorsAsync() {
        try {
            var doctors = await _unitOfWork.DoctorRepository.GetAllDoctors();

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
}