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

public class AppointmentsViewModel : ViewModelBase {
    private readonly AppointmentStore _appointmentStore;
    
    private readonly ObservableCollection<AppointmentDto> _appointments;
    private ObservableCollection<AppointmentDto> _filteredAppointments;
    private ObservableCollection<AppointmentDto> PageCollection { get; set; }
    public ICollectionView AppointmentsView { get; set; }

    private int _currentPage;
    private AppointmentStatus _activeFilter = AppointmentStatus.Accepted;
    
    public ObservableCollection<SortingOptions> SortingOptionsList { get; } = [
        SortingOptions.DateAscending,
        SortingOptions.DateDescending
    ];
    
    private SortingOptions _selectedSortingOption;
    public SortingOptions SelectedSortingOption {
        get => _selectedSortingOption;
        set {
            _selectedSortingOption = value;
            OnPropertyChanged();
            SortAppointments();
            AppointmentsView.Refresh();
        }
    }
    
    public int CurrentPageDisplay => _currentPage + 1;
    private int AmountOfItemsToSkip { get; set; }

    private AppointmentStatus ActiveFilter {
        get => _activeFilter;
        set {
            _activeFilter = value;
            OnPropertyChanged();
            
            ApplyFilter();
        }
    }

    public int UpdateAppointmentStatusId { get; set; }

    private ICommand LoadAppointmentsCommand { get; set; }
    public ICommand AppendFilterCommand { get; set; }
    public ICommand AcceptAppointmentCommand { get; set; }
    public ICommand CancelAppointmentCommand { get; set; }
    public ICommand ShowAddNewAppointmentModalCommand { get; set; }

    private AppointmentsViewModel(AppointmentStore appointmentStore, ModalNavigationViewModel modalNavigationViewModel){
        _appointmentStore = appointmentStore;
        _appointments = [];
        _filteredAppointments = new ObservableCollection<AppointmentDto>(_appointments);
        
        PageCollection = [];
        AppointmentsView = CollectionViewSource.GetDefaultView(PageCollection);
        
        LoadAppointmentsCommand = new LoadAppointmentsCommand(this, _appointmentStore);
        AppendFilterCommand = new RelayCommand<string>(SetFilter);
        AcceptAppointmentCommand = new UpdateAppointmentStatusCommand(this, _appointmentStore, AppointmentStatus.Accepted);
        CancelAppointmentCommand = new UpdateAppointmentStatusCommand(this, _appointmentStore, AppointmentStatus.Canceled);
        ShowAddNewAppointmentModalCommand = modalNavigationViewModel.ShowAddNewAppointmentModal;
            
        ApplyFilter();
        
        _appointmentStore.AppointmentStatusUpdated += OnAppointmentStatusUpdated;
        _appointmentStore.AppointmentCreated += OnAppointmentCreated;
    }

    public static AppointmentsViewModel LoadAppointmentsViewModel(AppointmentStore appointmentStore, ModalNavigationViewModel modalNavigationViewModel){
        var appointmentsViewModel = new AppointmentsViewModel(appointmentStore, modalNavigationViewModel);
        
        appointmentsViewModel.LoadAppointmentsCommand.Execute(null);
        
        return appointmentsViewModel;
    }

    public void UpdateAppointments(IEnumerable<AppointmentDto> appointments){
        _appointments.Clear();

        foreach (var appointment in appointments) {
            _appointments.Add(appointment);
        }
        
        SelectedSortingOption = SortingOptions.DateAscending;
        ApplyFilter();
    }

    private void OnAppointmentStatusUpdated(AppointmentDto appointmentDto){
        var foundAppointment = _appointments.FirstOrDefault(a => a.Id == appointmentDto.Id);
        if (foundAppointment != null) {
            foundAppointment.AppointmentStatus = appointmentDto.AppointmentStatus;
        }
    }

    private void OnAppointmentCreated(AppointmentDto appointmentDto) {
        _filteredAppointments.Add(appointmentDto);
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

    private void SortAppointments(){
        ApplySortingOptions.ApplySortingWithOneProperty(
            AppointmentsView,
            SelectedSortingOption,
            nameof(AppointmentDto.AppointmentDate)
            );
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
            _appointments.Where(dto => 
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
            AppointmentsView.Refresh();
            return;
        }
        
        var paginatedItems = _filteredAppointments.Skip(AmountOfItemsToSkip).Take(6).ToList();
        PageCollection.Clear();

        foreach (var item in paginatedItems) {
            PageCollection.Add(item);
        }
        
        OnPropertyChanged(nameof(CurrentPageDisplay));
        AppointmentsView.Refresh();
    }
}