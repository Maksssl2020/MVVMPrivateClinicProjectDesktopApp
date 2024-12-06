using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class ReferralStore {
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly List<ReferralDto> _referralsDto;
    private readonly Lazy<Task> _initializeLazy;

    public IEnumerable<ReferralDto> ReferralsDto => _referralsDto;

    public ReferralStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;
        
        _referralsDto = [];
        _initializeLazy = new Lazy<Task>(InitializeReferrals);
    }

    public async Task LoadReferrals(){
        await _initializeLazy.Value;
    }

    private async Task InitializeReferrals(){
        var loadedReferralsDto = await _unitOfWork.ReferralRepository.GetAllReferralsDtoAsync();
        
        _referralsDto.Clear();
        _referralsDto.AddRange(loadedReferralsDto);
    }
}