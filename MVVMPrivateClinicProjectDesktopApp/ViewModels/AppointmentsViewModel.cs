using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Channels;
using System.Windows.Data;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using static System.Enum;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class AppointmentsViewModel : ViewModelBase {
    private readonly ObservableCollection<AppointmentDto> _appointments;
    private ObservableCollection<AppointmentDto> _filteredAppointments;

    private int _currentPage;
    private AppointmentStatus _activeFilter = AppointmentStatus.Accepted;
    
    private ObservableCollection<AppointmentDto> PageCollection { get; set; }
    public ICollectionView AppointmentsView { get; set; }

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

    public ICommand AppendFilterCommand { get; set; }
    public ICommand AcceptAppointmentCommand { get; set; }
    public ICommand CancelAppointmentCommand { get; set; }
    
    public AppointmentsViewModel(){
        _appointments = new ObservableCollection<AppointmentDto>(GenerateMockData());
        _filteredAppointments = new ObservableCollection<AppointmentDto>(_appointments);
        
        PageCollection = [];
        AppointmentsView = CollectionViewSource.GetDefaultView(PageCollection);
        
        AppendFilterCommand = new RelayCommand<string>(SetFilter);
        
        ApplyFilter();
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
    
    private static List<AppointmentDto> GenerateMockData() {
        var random = new Random();
        return Enumerable.Range(1, 20).Select(i => new AppointmentDto {
            Id = i,
            DoctorFirstName = "Jane",
            DoctorLastName = "Smith",
            DoctorSpecialization = "Radiolog",
            PatientFirstName = "John",
            PatientLastName = "Doe",
            PatientCode = $"PATJD{i}",
            AppointmentStatus = AppointmentStatus.Scheduled.ToString(),
            AppointmentDate = DateTime.Now.AddDays(random.Next(1, 10))
        }).ToList();
    }
}