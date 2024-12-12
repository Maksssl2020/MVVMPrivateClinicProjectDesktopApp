using System.Collections;
using System.Windows.Documents;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class ReferralStore {
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly List<ReferralDto> _referralsDto;
    private readonly Lazy<Task> _initializeLazy;
    private readonly List<ReferralDto> _selectedPatientReferralsDto;
    
    public IEnumerable<ReferralDto> ReferralsDto => _referralsDto;
    public IEnumerable<ReferralDto> SelectedPatientReferralsDto => _selectedPatientReferralsDto;
    
    private int _selectedPatientId;
    public int SelectedPatientId {
        get => _selectedPatientId;
        set {
            _selectedPatientId = value;
            _selectedPatientReferralsDto.Clear();
        }
    }
    
    public event Action<ReferralDto>? ReferralCreated;

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
        _initializeLazy = new Lazy<Task>(InitializeReferrals);
    }

    public async Task LoadReferrals(){
        await _initializeLazy.Value;
    }

    public async Task LoadPatientReferrals(){
        var loadedPatientReferrals = await _unitOfWork.ReferralRepository.GetPatientAllReferralsAsync(SelectedPatientId);
        _selectedPatientReferralsDto.AddRange(loadedPatientReferrals);
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