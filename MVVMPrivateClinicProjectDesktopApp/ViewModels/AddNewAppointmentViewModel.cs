using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using System.Windows.Input;
using MVVMPrivateClinicProjectDesktopApp.Commands;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using ValidationResult = System.Windows.Controls.ValidationResult;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class AddNewAppointmentViewModel : ViewModelBase, IDoctorsViewModel, IPatientViewModel, IPricingViewModel {
    private readonly AppointmentDateStore _appointmentDateStore;

    private readonly ObservableCollection<DoctorDto> _doctors = [];
    private readonly ObservableCollection<PatientDto> _patients = [];
    private readonly ObservableCollection<PricingDto> _pricing = [];
    private readonly Dictionary<DateOnly, List<TimeOnly>> _datesToChoose;
    private Dictionary<DateOnly, List<TimeOnly>> _filteredDates = [];
    
    public ICollectionView DoctorsView { get; set; }
    public ICollectionView PatientsView { get; set; }
    public ICollectionView PricingView { get; set; }
    public ICollectionView DaysView { get; set; }
    public ICollectionView TimesView { get; set; }
    
    private DoctorDto _selectedDoctor;
    
    [Required(ErrorMessage = "Select a doctor!")]
    public DoctorDto SelectedDoctor {
        get => _selectedDoctor;
        set {
            _selectedDoctor = value;
            Validate(nameof(SelectedDoctor), value);
            SubmitCommand.OnCanExecuteChanged();
            OnDoctorOrPatientSelected();
        }
    }
    
    private PatientDto _selectedPatient;
    
    [Required(ErrorMessage = "Select a patient!")]
    public PatientDto SelectedPatient {
        get => _selectedPatient;
        set {
            _selectedPatient = value;
            Validate(nameof(SelectedPatient), value);
            SubmitCommand.OnCanExecuteChanged();
            OnDoctorOrPatientSelected();
            OnSelectionChanged();
        }
    }
    
    private PricingDto _selectedPricing;
    
    [Required(ErrorMessage = "Select a pricing!")]
    public PricingDto SelectedPricing {
        get => _selectedPricing;
        set {
            _selectedPricing = value;
            Validate(nameof(SelectedPricing), value);
            SubmitCommand.OnCanExecuteChanged();
        }
    }
    
    private DateOnly? _selectedDate;
    
    [Required(ErrorMessage = "Select a date!")]
    public DateOnly? SelectedDay { 
        get => _selectedDate;
        set {
            _selectedDate = value;
            Validate(nameof(SelectedDay), value!);
            SubmitCommand.OnCanExecuteChanged();
            TimesView = null!;

            if (_selectedDate == null) return;
            TimesView = CollectionViewSource.GetDefaultView(_datesToChoose[_selectedDate.Value]);
            OnSelectionChanged();
            OnPropertyChanged(nameof(TimesView));
        }
    }

    private TimeOnly? _selectedTime;
    
    [Required(ErrorMessage = "Select a time!")]
    public TimeOnly? SelectedTime {
        get => _selectedTime;
        set {
            _selectedTime = value;
            Validate(nameof(SelectedTime), value!);
            SubmitCommand.OnCanExecuteChanged();
            OnSelectionChanged();
        }
    }
    
    public bool IsDateSelectorEnabled => SelectedDoctor != null && SelectedPatient != null;
    public bool IsTimeSelectorEnabled => TimesView is { IsEmpty: false };
    
    private ICommand LoadDoctorsCommand { get; set; }
    private ICommand LoadPatientsCommand { get; set; }
    private ICommand LoadPricingCommand { get; set; }
    public SubmitCommand SubmitCommand { get; set; }
    public ICommand CreateAppointmentCommand { get; set; }

    private AddNewAppointmentViewModel(DoctorStore doctorStore, PatientStore patientStore, AppointmentStore appointmentStore,  AppointmentDateStore appointmentDateStore, PricingStore pricingStore, InvoiceStore invoiceStore){
        _appointmentDateStore = appointmentDateStore;
        _datesToChoose = GenerateDaysToChoose();
        
        DoctorsView = CollectionViewSource.GetDefaultView(_doctors);
        PatientsView = CollectionViewSource.GetDefaultView(_patients);
        PricingView = CollectionViewSource.GetDefaultView(_pricing);
        DaysView = null!;
        TimesView = null!;

        LoadDoctorsCommand = new LoadDoctorsCommand(this, doctorStore);
        LoadPatientsCommand = new LoadPatientsCommand(this, patientStore);
        LoadPricingCommand = new LoadPricingDtoCommand(this, pricingStore);
        SubmitCommand = new SubmitCommand(Submit, CanSubmit);
        CreateAppointmentCommand =
            new CreateAppointmentCommand(this, appointmentStore, appointmentDateStore, invoiceStore);
    }

    private bool CanSubmit(){
        var context = new ValidationContext(this);
        var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
        return Validator.TryValidateObject(this, context, results, true);
    }

    private void Submit(){
        Console.WriteLine("SSSUBMIT!");
    }

    public static AddNewAppointmentViewModel LoadAddNewAppointmentViewModel(DoctorStore doctorStore, PatientStore patientStore, AppointmentStore appointmentStore,  AppointmentDateStore appointmentDateStore, PricingStore pricingStore, InvoiceStore invoiceStore){
        var addNewAppointmentViewModel = new AddNewAppointmentViewModel(doctorStore, patientStore, appointmentStore, appointmentDateStore, pricingStore, invoiceStore);
        
        addNewAppointmentViewModel.LoadDoctorsCommand.Execute(null);
        addNewAppointmentViewModel.LoadPatientsCommand.Execute(null);
        addNewAppointmentViewModel.LoadPricingCommand.Execute(null);
        
        return addNewAppointmentViewModel;
    }
    
    public void UpdateDoctorsDto(IEnumerable<DoctorDto> doctorsDto){
        _doctors.Clear();

        foreach (var doctorDto in doctorsDto) {
            _doctors.Add(doctorDto);
        }
    }
    
    public void UpdatePatients(IEnumerable<PatientDto> patients){
        _patients.Clear();

        foreach (var patientDto in patients) {
            _patients.Add(patientDto);
        }
    }

    public void UpdatePricing(IEnumerable<PricingDto> pricingDto){
        _pricing.Clear();

        foreach (var pricing in pricingDto) {
            _pricing.Add(pricing);
        }
    }
    
    private void OnSelectionChanged(){
        OnPropertyChanged(nameof(IsDateSelectorEnabled));
        OnPropertyChanged(nameof(IsTimeSelectorEnabled));
    }

    private async void OnDoctorOrPatientSelected(){
        try {
            if (SelectedDoctor == null || SelectedPatient == null) return;
        
            await FilterDates(SelectedDoctor.Id, AppointmentDatePersonType.Doctor);
            await FilterDates(SelectedPatient.Id, AppointmentDatePersonType.Patient);
            
            DaysView = CollectionViewSource.GetDefaultView(_filteredDates.Keys);
            OnPropertyChanged(nameof(DaysView));
            OnPropertyChanged(nameof(TimesView));
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
        }
    }
    
    private async Task FilterDates(int personId, AppointmentDatePersonType appointmentDatePersonType){
        var chosenPersonAppointmentsDates =
            await _appointmentDateStore.GetChosenPersonAppointmentsDates(personId, appointmentDatePersonType);
        
        _filteredDates = _datesToChoose;

        if (chosenPersonAppointmentsDates.Count == 0) {
            return;
        }
        
        foreach (var appointmentsDate in chosenPersonAppointmentsDates) {
            var dateOnly = DateOnly.FromDateTime(appointmentsDate);
            var timeOnly = TimeOnly.FromDateTime(appointmentsDate);

            if (_filteredDates.TryGetValue(dateOnly, out var value)) {
                value.Remove(timeOnly);
            }
        }
    }
    
    private static Dictionary<DateOnly, List<TimeOnly>> GenerateDaysToChoose(){
        var daysToChoose = new Dictionary<DateOnly, List<TimeOnly>>();
        var currentDay = DateOnly.FromDateTime(DateTime.Now);
        var currentTime = TimeOnly.FromDateTime(DateTime.Now);

        for (var i = 0; i <= 7; i++) {
            var date = currentDay.AddDays(i);

            switch (i) {
                case 0 when currentTime.Hour < 16:
                    Console.WriteLine(currentTime.Hour);
                    daysToChoose.Add(date, GenerateTimeToChoose(true));
                    break;
                case > 0:
                    daysToChoose.Add(date, GenerateTimeToChoose(false));
                    break;
            }
        }
        
        return daysToChoose;
    }
    
    private static List<TimeOnly> GenerateTimeToChoose(bool isToday){
        var currentTime = new TimeOnly(DateTime.Now.Hour, DateTime.Now.Minute, 0);
        var startTime = new TimeOnly(8, 0, 0);
        var endTime = new TimeOnly(16, 0, 0);
        const int interval = 15;
        
        List<TimeOnly> generatedTimes = [];

        while (startTime <= endTime) {
            generatedTimes.Add(startTime);
            startTime = startTime.AddMinutes(interval);
        }

        if (!isToday) return generatedTimes;
        
        for (var i = 0; i < generatedTimes.Count; i++) {
            if (generatedTimes[i] < currentTime) {
                generatedTimes.RemoveAt(i);
            }
        }
            
        return generatedTimes;
    }
}