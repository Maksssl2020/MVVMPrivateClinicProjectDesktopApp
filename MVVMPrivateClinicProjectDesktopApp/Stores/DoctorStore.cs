using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class DoctorStore : EntityStore<DoctorDto, DoctorStatisticsDto> {
    private readonly List<DoctorDto> _familyMedicineDoctorsDto;
    private readonly List<DoctorDto> _mostPopularDoctorsDto;
    private readonly Lazy<Task> _initializeLazyFamilyMedicineDoctors;
    private readonly Lazy<Task> _initializeLazyMostPopularDoctors;

    public IEnumerable<DoctorDto> FamilyMedicineDoctorsDto => _familyMedicineDoctorsDto;
    public IEnumerable<DoctorDto> MostPopularDoctorsDto => _mostPopularDoctorsDto;
    
    public DoctorStore(IUnitOfWork unitOfWork) 
        :base(unitOfWork) {
        _familyMedicineDoctorsDto = [];
        _mostPopularDoctorsDto = [];
        
        _initializeLazyFamilyMedicineDoctors = new Lazy<Task>(InitializeFamilyMedicineDoctorsDto);
        _initializeLazyMostPopularDoctors = new Lazy<Task>(InitializeMostPopularDoctorsDto);
    }

    public async Task LoadFamilyMedicineDoctorsDto(){
        await _initializeLazyFamilyMedicineDoctors.Value;
    }

    public async Task LoadMostPopularDoctorsDto(){
        await _initializeLazyMostPopularDoctors.Value;
    }
    
    public override async Task CreateEntity(object entityRequest){
        if (entityRequest is SaveDoctorRequest saveDoctorRequest) {
            var savedDoctor = await UnitOfWork.DoctorRepository.SaveDoctorAsync(saveDoctorRequest);
            Entities.Add(savedDoctor);
        
            OnEntityCreated(savedDoctor);
        }
    }

    public override async Task DeleteEntity(int entityId){
        await UnitOfWork.DoctorRepository.DeleteEntityAsync(entityId);
        Entities.RemoveAll(e => e.Id == entityId);
        OnEntityDeleted(entityId);
    }

    public override async Task LoadEntityDetails(){
        var foundDoctor = await UnitOfWork.DoctorRepository.GetDoctorByIdAsync(EntityIdToShowDetails);
        var countAppointments = await UnitOfWork.AppointmentRepository.CountAppointmentsByDoctorIdAsync(EntityIdToShowDetails);
        var countReferrals = await UnitOfWork.ReferralRepository.CountIssuedReferralsByDoctorIdAsync(EntityIdToShowDetails);
        var countPrescriptions = await UnitOfWork.PrescriptionRepository.CountIssuedPrescriptionsByDoctorIdAsync(EntityIdToShowDetails);
        var countDiagnosis = await UnitOfWork.DiagnosesRepository.CountIssuedDiagnosisByDoctorIdAsync(EntityIdToShowDetails);
        var countPatientNotes = await UnitOfWork.PatientNoteRepository.CountIssuedPatientNotesByDoctorIdAsync(EntityIdToShowDetails);

        SelectedEntityDetails = new DoctorStatisticsDto {
            DoctorCode = foundDoctor!.DoctorCode,
            AmountOfAppointments = countAppointments,
            IssuedReferrals = countReferrals,
            IssuedPrescriptions = countPrescriptions,
            IssuedDiagnosis = countDiagnosis,
            IssuedPatientNotes = countPatientNotes
        };
    }

    private async Task InitializeFamilyMedicineDoctorsDto(){
        var allFamilyMedicineDoctors = await UnitOfWork.DoctorRepository.GetAllFamilyMedicineDoctors();
        
        _familyMedicineDoctorsDto.Clear();
        _familyMedicineDoctorsDto.AddRange(allFamilyMedicineDoctors);
    }

    private async Task InitializeMostPopularDoctorsDto(){
        var loadedMostPopularDoctors = await UnitOfWork.DoctorRepository.GetMostPopularDoctors(4);
        
        _mostPopularDoctorsDto.Clear();
        _mostPopularDoctorsDto.AddRange(loadedMostPopularDoctors);
    }
    
    protected override async Task InitializeEntities(){
        var loadedDoctors = await UnitOfWork.DoctorRepository.GetAllDoctors();
        
        Entities.Clear();
        Entities.AddRange(loadedDoctors);
    }
}