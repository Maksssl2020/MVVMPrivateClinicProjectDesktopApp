using System.Windows.Documents;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class ReferralTestStore {
    private readonly IUnitOfWork _unitOfWork;

    private readonly List<ReferralTestDto> _referralTests;
    private readonly Lazy<Task> _initializeLazyReferralTests;

    public IEnumerable<ReferralTestDto> ReferralTests => _referralTests;
    
    private int _selectedReferralTestId;
    public int SelectedReferralTestId {
        get => _selectedReferralTestId;
        set {
            _selectedReferralTestId = value;
            ReferralTestDetails = null!;
        }
    }

    public ReferralTestDetailsDto ReferralTestDetails { get; set; } = null!;
    
    public ReferralTestStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;

        _referralTests = [];
        _initializeLazyReferralTests = new Lazy<Task>(InitializeReferralTests);
    }

    public async Task LoadReferralTests(){
        await _initializeLazyReferralTests.Value;
    }

    public async Task LoadReferralTest(){
        var foundReferral = await _unitOfWork.ReferralTestRepository.GetReferralTestDetailsByIdAsync(SelectedReferralTestId);

        if (foundReferral != null) {
            var referralTestUses = await _unitOfWork.ReferralRepository.CountReferralTestUsesAsync(SelectedReferralTestId);
            
            foundReferral.TotalUses = referralTestUses;
            ReferralTestDetails = foundReferral;
        }
    }
    
    private async Task InitializeReferralTests(){
        var loadedReferralTests = await _unitOfWork.ReferralTestRepository.GetAllReferralTestsDtoAsync();
        
        _referralTests.Clear();
        _referralTests.AddRange(loadedReferralTests);
    }
}