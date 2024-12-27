using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Windows.Documents;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class ReferralStore(IUnitOfWork unitOfWork) : EntityStore<ReferralDto, ReferralDetailsDto>(unitOfWork) {
    private readonly List<ReferralDto> _selectedPatientReferralsDto = [];
    private readonly List<ReferralDto> _selectedDoctorIssuedReferralsDto = [];
    
    public IEnumerable<ReferralDto> SelectedPatientReferralsDto => _selectedPatientReferralsDto;
    public IEnumerable<ReferralDto> SelectedDoctorIssuedReferrals => _selectedDoctorIssuedReferralsDto;
    
    
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

    public async Task LoadPatientReferrals(){
        var loadedPatientReferrals = await UnitOfWork.ReferralRepository.GetIssuedReferralsByPatientIdOrDoctorId(SelectedPatientId, PersonType.Patient);
        _selectedPatientReferralsDto.AddRange(loadedPatientReferrals);
    }

    public async Task LoadDoctorIssuedReferrals(){
        var foundReferrals = await UnitOfWork.ReferralRepository.GetIssuedReferralsByPatientIdOrDoctorId(SelectedPatientId, PersonType.Doctor);
        _selectedDoctorIssuedReferralsDto.AddRange(foundReferrals);
    }

    public override async Task CreateEntity(object entityRequest){
        if (entityRequest is SaveReferralRequest saveReferralRequest) {
            var savedReferral = await UnitOfWork.ReferralRepository.SaveReferralAsync(saveReferralRequest);
            Entities.Add(savedReferral);
            _selectedPatientReferralsDto.Add(savedReferral);

            OnEntityCreated(savedReferral);
        }
    }

    public override async Task DeleteEntity(int entityId){
        await UnitOfWork.ReferralRepository.DeleteEntityAsync(entityId);
        Entities.RemoveAll(e => e.Id == entityId);
        OnEntityDeleted(entityId);
    }

    public override async Task LoadEntityDetails(){
        var foundReferral = await UnitOfWork.ReferralRepository.GetReferralDetailsByIdAsync(EntityIdToShowDetails);
        if (foundReferral != null) SelectedEntityDetails = foundReferral;
    }

    public async Task GenerateReferralDetailsPdf(int referralId){
        var foundReferral = await UnitOfWork.ReferralRepository.GetReferralDetailsByIdAsync(referralId);
        if (foundReferral != null) {
            PdfGenerator.GenerateReferralPdf(foundReferral);
        }
    }
    
    protected override async Task InitializeEntities(){
        var loadedReferralsDto = await UnitOfWork.ReferralRepository.GetAllReferralsDtoAsync();
        
        Entities.Clear();
        Entities.AddRange(loadedReferralsDto);
    }
}