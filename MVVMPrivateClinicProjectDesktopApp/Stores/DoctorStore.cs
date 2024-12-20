using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class DoctorStore {
    private readonly IUnitOfWork _unitOfWork;

    private readonly List<DoctorDto> _allDoctorsDto;
    private readonly List<DoctorDto> _familyMedicineDoctorsDto;
    private readonly List<DoctorDto> _mostPopularDoctorsDto;
    private readonly Lazy<Task> _initializeLazyAllDoctors;
    private readonly Lazy<Task> _initializeLazyFamilyMedicineDoctors;
    private readonly Lazy<Task> _initializeLazyMostPopularDoctors;

    public IEnumerable<DoctorDto> AllDoctorsDto => _allDoctorsDto;
    public IEnumerable<DoctorDto> FamilyMedicineDoctorsDto => _familyMedicineDoctorsDto;
    public IEnumerable<DoctorDto> MostPopularDoctorsDto => _mostPopularDoctorsDto;

    private int _selectedDoctorId;
    public int SelectedDoctorId {
        get => _selectedDoctorId;
        set {
            _selectedDoctorId = value;
            DoctorStatisticsDto = null!;
        }
    }

    public DoctorStatisticsDto DoctorStatisticsDto { get; set; } = null!;
    
    public event Action<DoctorDto>? DoctorCreated;
    
    public DoctorStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;
        
        _allDoctorsDto = [];
        _familyMedicineDoctorsDto = [];
        _mostPopularDoctorsDto = [];
        
        _initializeLazyAllDoctors = new Lazy<Task>(InitializeAllDoctorsDto);
        _initializeLazyFamilyMedicineDoctors = new Lazy<Task>(InitializeFamilyMedicineDoctorsDto);
        _initializeLazyMostPopularDoctors = new Lazy<Task>(InitializeMostPopularDoctorsDto);
    }

    public async Task LoadAllDoctorsDto(){
        await _initializeLazyAllDoctors.Value;
    }

    public async Task LoadFamilyMedicineDoctorsDto(){
        await _initializeLazyFamilyMedicineDoctors.Value;
    }

    public async Task LoadMostPopularDoctorsDto(){
        await _initializeLazyMostPopularDoctors.Value;
    }
    
    public async Task CreateDoctor(SaveDoctorRequest doctorRequest){
        var savedDoctor = await _unitOfWork.DoctorRepository.SaveDoctorAsync(doctorRequest);
        _allDoctorsDto.Add(savedDoctor);
        
        OnDoctorCreated(savedDoctor);
    }

    public async Task LoadDoctorStatistics(){
        var countAppointments = await _unitOfWork.AppointmentRepository.CountAppointmentsByDoctorIdAsync(SelectedDoctorId);
        var countReferrals = await _unitOfWork.ReferralRepository.CountIssuedReferralsByDoctorIdAsync(SelectedDoctorId);
        var countPrescriptions = await _unitOfWork.PrescriptionRepository.CountIssuedPrescriptionsByDoctorIdAsync(SelectedDoctorId);
        var countDiagnosis = await _unitOfWork.DiagnosisRepository.CountIssuedDiagnosisByDoctorIdAsync(SelectedDoctorId);
        var countPatientNotes = await _unitOfWork.PatientNoteRepository.CountIssuedPatientNotesByDoctorIdAsync(SelectedDoctorId);

        DoctorStatisticsDto = new DoctorStatisticsDto {
            AmountOfAppointments = countAppointments,
            IssuedReferrals = countReferrals,
            IssuedPrescriptions = countPrescriptions,
            IssuedDiagnosis = countDiagnosis,
            IssuedPatientNotes = countPatientNotes
        };
    }
    
    private void OnDoctorCreated(DoctorDto doctor){
        DoctorCreated?.Invoke(doctor);
    }
    
    private async Task InitializeAllDoctorsDto(){
        var loadedDoctors = await _unitOfWork.DoctorRepository.GetAllDoctors();
        
        _allDoctorsDto.Clear();
        _allDoctorsDto.AddRange(loadedDoctors);
    }

    private async Task InitializeFamilyMedicineDoctorsDto(){
        var allFamilyMedicineDoctors = await _unitOfWork.DoctorRepository.GetAllFamilyMedicineDoctors();
        
        _familyMedicineDoctorsDto.Clear();
        _familyMedicineDoctorsDto.AddRange(allFamilyMedicineDoctors);
    }

    private async Task InitializeMostPopularDoctorsDto(){
        var loadedMostPopularDoctors = await _unitOfWork.DoctorRepository.GetMostPopularDoctors(4);
        
        _mostPopularDoctorsDto.Clear();
        _mostPopularDoctorsDto.AddRange(loadedMostPopularDoctors);
    }
}