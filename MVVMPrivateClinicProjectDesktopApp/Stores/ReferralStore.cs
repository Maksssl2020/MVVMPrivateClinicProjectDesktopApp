using System.Collections;
using System.Windows.Documents;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class ReferralStore {
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly List<ReferralDto> _referralsDto;
    private readonly List<ReferralDto> _selectedPatientReferralsDto;
    private readonly List<ReferralDto> _selectedDoctorIssuedReferralsDto;
    private readonly Lazy<Task> _initializeLazy;
    
    public IEnumerable<ReferralDto> ReferralsDto => _referralsDto;
    public IEnumerable<ReferralDto> SelectedPatientReferralsDto => _selectedPatientReferralsDto;
    public IEnumerable<ReferralDto> SelectedDoctorIssuedReferrals => _selectedDoctorIssuedReferralsDto;
    
    public event Action<ReferralDto>? ReferralCreated;
    
    private int _selectedPatientId;
    public int SelectedPatientId {
        get => _selectedPatientId;
        set {
            _selectedPatientId = value;
            _selectedPatientReferralsDto.Clear();
        }
    }
    
    private int _selectedDoctorId;
    public int SelectedDoctorId {
        get => _selectedDoctorId;
        set {
            _selectedDoctorId = value;
            _selectedDoctorIssuedReferralsDto.Clear();
        }
    }

    private int _selectedReferralId;
    public int SelectedReferralId {
        get => _selectedReferralId;
        set {
            _selectedReferralId = value;
            SelectedReferralDetails = null!;
        }
    }

    public ReferralDetailsDto SelectedReferralDetails { get; set; } = null!;
    
    public ReferralStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;
        
        _referralsDto = [];
        _selectedPatientReferralsDto = [];
        _selectedDoctorIssuedReferralsDto = [];
        _initializeLazy = new Lazy<Task>(InitializeReferrals);
    }

    public async Task LoadReferrals(){
        await _initializeLazy.Value;
    }

    public async Task LoadPatientReferrals(){
        var loadedPatientReferrals = await _unitOfWork.ReferralRepository.GetIssuedReferralsByPatientIdOrDoctorId(SelectedPatientId, PersonType.Patient);
        _selectedPatientReferralsDto.AddRange(loadedPatientReferrals);
    }

    public async Task LoadDoctorIssuedReferrals(){
        var foundReferrals = await _unitOfWork.ReferralRepository.GetIssuedReferralsByPatientIdOrDoctorId(SelectedPatientId, PersonType.Doctor);
        _selectedDoctorIssuedReferralsDto.AddRange(foundReferrals);
    }
    
    public async Task CreateReferral(SaveReferralRequest referralRequest){
        var savedReferral = await _unitOfWork.ReferralRepository.SaveReferralAsync(referralRequest);
        _referralsDto.Add(savedReferral);
        _selectedPatientReferralsDto.Add(savedReferral);

        OnReferralCreated(savedReferral);
    }

    public async Task LoadReferralDetails(){
        var foundReferral = await _unitOfWork.ReferralRepository.GetReferralDetailsByIdAsync(SelectedReferralId);
        if (foundReferral != null) SelectedReferralDetails = foundReferral;
    }
    
    private void OnReferralCreated(ReferralDto savedReferral){
        ReferralCreated?.Invoke(savedReferral);
    }

    private async Task InitializeReferrals(){
        var loadedReferralsDto = await _unitOfWork.ReferralRepository.GetAllReferralsDtoAsync();
        
        _referralsDto.Clear();
        _referralsDto.AddRange(loadedReferralsDto);
    }
}