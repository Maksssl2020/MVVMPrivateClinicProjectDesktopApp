using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.Views;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class DoctorsViewModel : DisplayEntitiesViewModelBase<DoctorDto> {
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
    
    private DoctorsViewModel(DoctorStore doctorStore, ModalNavigationViewModel modalNavigationViewModel)
        :base([SortingOptions.AlphabeticalAscending, SortingOptions.AlphabeticalDescending, SortingOptions.IdAscending, SortingOptions.IdDescending]) {
        LoadDoctorsCommand = new LoadDoctorsCommand(UpdateEntities, doctorStore);
        ShowAddNewDoctorViewCommand = modalNavigationViewModel.ShowAddNewDoctorModal;
        doctorStore.DoctorCreated += OnDoctorCreated;
    }

    public static DoctorsViewModel LoadDoctorsViewModel(DoctorStore doctorStore, ModalNavigationViewModel modalNavigationViewModel){
        var doctorsViewModel = new DoctorsViewModel(doctorStore, modalNavigationViewModel);
        
        doctorsViewModel.LoadDoctorsCommand.Execute(null);
        
        return doctorsViewModel;
    }

    public override void UpdateEntities(IEnumerable<DoctorDto> entities){
        Entities.Clear();

        foreach (var entity in entities) {
            Entities.Add(entity);
        }
        
        SelectedSortingOption = SortingOptions.AlphabeticalAscending;
    }

    protected override void SortEntities(){
        if (SelectedSortingOption is SortingOptions.AlphabeticalAscending or SortingOptions.AlphabeticalDescending) {
            ApplySortingOptions.ApplySortingWithTwoProperties(EntitiesView, SelectedSortingOption, nameof(DoctorDto.FirstName), nameof(DoctorDto.LastName));
        }
        else {
            ApplySortingOptions.ApplySortingWithOneProperty(EntitiesView, SelectedSortingOption, nameof(DoctorDto.Id));
        }
    }

    protected override bool ApplyFilter(object obj){
        if (obj is not DoctorDto doctorDto) {
            return false;
        }

        if (string.IsNullOrWhiteSpace(Filter)) {
            return true;
        }
        
        var filter = Filter.Trim().ToLower();
        return doctorDto.DoctorCode!.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
               doctorDto.FirstName.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
               doctorDto.LastName.Contains(filter, StringComparison.CurrentCultureIgnoreCase);
    }
    
    private void OnDoctorCreated(DoctorDto doctor){
        Entities.Add(doctor);
    }
}