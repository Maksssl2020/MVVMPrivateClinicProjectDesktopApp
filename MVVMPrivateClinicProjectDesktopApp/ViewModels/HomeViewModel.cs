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
    private readonly ObservableCollection<TopPricingDto> _topPricing;
    private readonly ObservableCollection<AppointmentDto> _upcomingAppointments;
    private readonly ObservableCollection<StatisticDto> _statistics;
    public ICollectionView MostPopularDoctorsView { get; set; }
    public ICollectionView TopPricingView { get; set; }
    public ICollectionView UpcomingAppointmentsView { get; set; }
    public ICollectionView Statistics { get; set; }

    private ICommand LoadMostPopularDoctorsCommand { get; set; }
    private ICommand LoadTopPricingCommand { get; set; }
    private ICommand LoadUpcomingAppointmentsCommand { get; set; }
    private ICommand LoadStatisticsCommand { get; set; }
    
    private HomeViewModel(DoctorStore doctorStore, PricingStore pricingStore, AppointmentStore appointmentStore, StatisticsStore statisticsStore){
        _mostPopularDoctors = [];
        _topPricing = [];
        _upcomingAppointments = [];
        _statistics = [];
        
        MostPopularDoctorsView = CollectionViewSource.GetDefaultView(_mostPopularDoctors);
        TopPricingView = CollectionViewSource.GetDefaultView(_topPricing);
        UpcomingAppointmentsView = CollectionViewSource.GetDefaultView(_upcomingAppointments); 
        Statistics = CollectionViewSource.GetDefaultView(_statistics);
        
        LoadMostPopularDoctorsCommand = new LoadMostPopularDoctorsCommand(this, doctorStore);
        LoadTopPricingCommand = new LoadTopPricingCommand(this, pricingStore);
        LoadUpcomingAppointmentsCommand = new LoadUpcomingAppointmentsCommand(this, appointmentStore);
        LoadStatisticsCommand = new LoadStatisticsCommand(this, statisticsStore);
    }

    public static HomeViewModel LoadHomeViewModel(DoctorStore doctorStore, PricingStore pricingStore, AppointmentStore appointmentStore, StatisticsStore statisticsStore){
        var homeViewModel = new HomeViewModel(doctorStore, pricingStore, appointmentStore, statisticsStore);
        
        homeViewModel.LoadMostPopularDoctorsCommand.Execute(null);
        homeViewModel.LoadTopPricingCommand.Execute(null);
        homeViewModel.LoadUpcomingAppointmentsCommand.Execute(null);
        homeViewModel.LoadStatisticsCommand.Execute(null);
        
        return homeViewModel;
    }
    
    public void UpdateMostPopularDoctors(IEnumerable<DoctorDto> mostPopularDoctorsDto){
        _mostPopularDoctors.Clear();

        foreach (var doctorDto in mostPopularDoctorsDto) {
            _mostPopularDoctors.Add(doctorDto);
        }
    }

    public void UpdateTopPricing(IEnumerable<TopPricingDto> pricingStoreTopPricingDto){
        var position = 1;
        _topPricing.Clear();

        foreach (var topPricingDto in pricingStoreTopPricingDto) {
            topPricingDto.Position = position;
            _topPricing.Add(topPricingDto);
            position++;
        }
    }

    public void UpdateUpcomingAppointments(IEnumerable<AppointmentDto> upcomingAppointments){
        _upcomingAppointments.Clear();

        foreach (var appointment in upcomingAppointments) {
            _upcomingAppointments.Add(appointment);
        }
    }

    public void UpdateStatistics(IEnumerable<StatisticDto> statistics){
        _statistics.Clear();

        foreach (var statistic in statistics) {
            _statistics.Add(statistic);
        }
    }
}
    
