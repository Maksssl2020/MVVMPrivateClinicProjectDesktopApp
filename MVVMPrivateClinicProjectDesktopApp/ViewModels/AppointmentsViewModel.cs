using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Channels;
using System.Windows.Data;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using static System.Enum;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class AppointmentsViewModel : DisplayEntitiesViewModelBase<AppointmentDto, AppointmentDto> {
    private ObservableCollection<AppointmentDto> _filteredAppointments;
    private ObservableCollection<AppointmentDto> PageCollection { get; }

    private int _currentPage;
    
    public int CurrentPageDisplay => _currentPage + 1;
    private int AmountOfItemsToSkip { get; set; }

    private AppointmentStatus _activeFilter = AppointmentStatus.Accepted;
    private AppointmentStatus ActiveFilter {
        get => _activeFilter;
        set {
            _activeFilter = value;
            OnPropertyChanged();
            ApplyFilter();
        }
    }

    public int UpdateAppointmentStatusId { get; set; }

    public ICommand AppendFilterCommand { get; set; }
    public ICommand AcceptAppointmentCommand { get; set; }
    public ICommand CancelAppointmentCommand { get; set; }
    public ICommand ShowAddNewAppointmentModalCommand { get; set; }
    public ICommand ShowAppointmentDetailsModalCommand { get; set; }

    private AppointmentsViewModel(AppointmentStore appointmentStore, ModalNavigationViewModel modalNavigationViewModel)
        :base([
            SortingOptions.DateAscending,
            SortingOptions.DateDescending
        ], appointmentStore, modalNavigationViewModel) {
        _filteredAppointments = new ObservableCollection<AppointmentDto>(Entities);
        PageCollection = [];
        EntitiesView = CollectionViewSource.GetDefaultView(PageCollection);
        
        AppendFilterCommand = new RelayCommand<string>(SetFilter);
        AcceptAppointmentCommand = new UpdateAppointmentStatusCommand(this, appointmentStore, AppointmentStatus.Accepted);
        CancelAppointmentCommand = new UpdateAppointmentStatusCommand(this, appointmentStore, AppointmentStatus.Canceled);
        ShowAddNewAppointmentModalCommand = modalNavigationViewModel.ShowAddNewAppointmentModal;
        ShowAppointmentDetailsModalCommand = modalNavigationViewModel.ShowAppointmentDetailsModal;
        
        ApplyFilter();
        
        appointmentStore.AppointmentStatusUpdated += OnAppointmentStatusUpdated;
    }

    public static AppointmentsViewModel LoadAppointmentsViewModel(AppointmentStore appointmentStore, ModalNavigationViewModel modalNavigationViewModel){
        var appointmentsViewModel = new AppointmentsViewModel(appointmentStore, modalNavigationViewModel);
        
        appointmentsViewModel.LoadEntitiesCommand.Execute(null);
        
        return appointmentsViewModel;
    }


    protected override void UpdateEntities(IEnumerable<AppointmentDto> entities){
        Entities.Clear();

        foreach (var appointment in entities) {
            Entities.Add(appointment);
        }
        
        SelectedSortingOption = SortingOptions.DateAscending;
        ApplyFilter();
    }

    protected override void SortEntities(){
        ApplySortingOptions.ApplySortingWithOneProperty(
            EntitiesView,
            SelectedSortingOption,
            nameof(AppointmentDto.AppointmentDate)
        );
    }

    protected override bool ApplyFilter(object obj){
        return true;
    }
    
    private void OnAppointmentStatusUpdated(AppointmentDto appointmentDto){
        var foundAppointment = Entities.FirstOrDefault(a => a.Id == appointmentDto.Id);
        if (foundAppointment != null) {
            foundAppointment.AppointmentStatus = appointmentDto.AppointmentStatus;
        }
    }
    
    public void NextPage(){
        if (AmountOfItemsToSkip + 6 >= _filteredAppointments.Count) return;
        
        _currentPage++;
        AmountOfItemsToSkip += 6;
        
        UpdatePageCollection();
    }

    public void BackPage() {
        if (_currentPage <= 0) return;
        
        _currentPage--;
        AmountOfItemsToSkip -= 6;
       
        UpdatePageCollection();
    }
    
    private void SetFilter(string? filter){
        if (string.IsNullOrEmpty(filter)) return;

        var result = TryParse(filter, out AppointmentStatus appointmentStatus);
        if (result) {
            ActiveFilter = appointmentStatus;
        }
    }

    private void ApplyFilter(){
       _filteredAppointments = new ObservableCollection<AppointmentDto>(
            Entities.Where(dto => 
                TryParse(dto.AppointmentStatus, out AppointmentStatus status) && 
                status == ActiveFilter)
            );

            _currentPage = 0;
            AmountOfItemsToSkip = 0;
            
            UpdatePageCollection();
    }
    
    private void UpdatePageCollection() {
        if (_filteredAppointments.Count == 0) {
            PageCollection.Clear();
            OnPropertyChanged(nameof(CurrentPageDisplay));
            EntitiesView.Refresh();
            return;
        }
        
        var paginatedItems = _filteredAppointments.Skip(AmountOfItemsToSkip).Take(6).ToList();
        PageCollection.Clear();

        foreach (var item in paginatedItems) {
            PageCollection.Add(item);
        }
        
        OnPropertyChanged(nameof(CurrentPageDisplay));
        EntitiesView.Refresh();
    }
}