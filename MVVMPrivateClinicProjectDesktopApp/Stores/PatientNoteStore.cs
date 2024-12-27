using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class PatientNoteStore(IUnitOfWork unitOfWork) : EntityStore<PatientNoteDto, PatientNoteDetailsDto>(unitOfWork) {
    private readonly List<PatientNoteWithDoctorDataDto> _selectedPatientNotes = [];
    private readonly List<PatientNoteWithDoctorDataDto> _selectedDoctorIssuedPatientNotes = [];
    
    public IEnumerable<PatientNoteWithDoctorDataDto> SelectedPatientNotes => _selectedPatientNotes;
    public IEnumerable<PatientNoteWithDoctorDataDto> SelectedDoctorIssuedPatientNotes => _selectedDoctorIssuedPatientNotes;

    private int _selectedPatientId;
    public int SelectedPatientId {
        get => _selectedPatientId;
        set {
            _selectedPatientId = value;
            _selectedPatientNotes.Clear();
        }
    }

    private int _selectedDoctorId;
    public int SelectedDoctorId {
        get => _selectedDoctorId;
        set {
            _selectedDoctorId = value;
            _selectedDoctorIssuedPatientNotes.Clear();
        }
    }

    public async Task LoadSelectedPatientNotes(){
        var foundPatientNotes =
            await UnitOfWork.PatientNoteRepository.GetIssuedPatientNotesByPatientOrDoctorId(SelectedPatientId,
                PersonType.Patient);
        _selectedPatientNotes.AddRange(foundPatientNotes);
    }

    public async Task LoadSelectedDoctorIssuedPatientNotes(){
        var foundPatientNotes =await UnitOfWork.PatientNoteRepository.GetIssuedPatientNotesByPatientOrDoctorId(SelectedDoctorId,
            PersonType.Doctor);
        _selectedDoctorIssuedPatientNotes.AddRange(foundPatientNotes);
    }

    public override async Task CreateEntity(object entityRequest){
        if (entityRequest is SavePatientNoteRequest savePatientNoteRequest) {
            var savedPatientNoteDto = await UnitOfWork.PatientNoteRepository.SavePatientNoteAsync(savePatientNoteRequest);
            Entities.Add(savedPatientNoteDto);

            OnEntityCreated(savedPatientNoteDto);
        }
    }

    public override async Task DeleteEntity(int entityId){
        await UnitOfWork.PatientNoteRepository.DeleteEntityAsync(entityId);
        Entities.RemoveAll(e => e.Id == entityId);
        OnEntityDeleted(entityId);
    }

    public override async Task LoadEntityDetails(){
        var loadedPatientNote = await UnitOfWork.PatientNoteRepository.GetPatientNoteDetailsAsync(EntityIdToShowDetails).ConfigureAwait(false);
        if (loadedPatientNote != null) SelectedEntityDetails = loadedPatientNote;
    }

    protected override async Task InitializeEntities(){
        var loadedPatientsNotes = await UnitOfWork.PatientNoteRepository.GetAllPatientsNotesAsync();
        
        Entities.Clear();
        Entities.AddRange(loadedPatientsNotes);
    }
}