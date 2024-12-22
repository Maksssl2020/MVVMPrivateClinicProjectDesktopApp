using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class PatientNoteStore {
    private readonly IUnitOfWork _unitOfWork;

    private readonly List<PatientNoteDto> _allPatientsNotesDto;
    private readonly List<PatientNoteWithDoctorDataDto> _selectedPatientNotes;
    private readonly List<PatientNoteWithDoctorDataDto> _selectedDoctorIssuedPatientNotes;
    
    private readonly Lazy<Task> _initializeAllPatientsNotesLazy;

    public IEnumerable<PatientNoteDto> AllPatientsNotesDto => _allPatientsNotesDto;
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
    
    private int _selectedPatientNoteId;
    public int SelectedPatientNoteId {
        get => _selectedPatientNoteId;
        set {
            _selectedPatientNoteId = value;
            SelectedPatientNote = null!;
        }
    }

    
    public PatientNoteDetailsDto SelectedPatientNote { get; private set; } = null!;

    public event Action<PatientNoteDto>? PatientNoteCreated;
    
    public PatientNoteStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;

        _allPatientsNotesDto = [];
        _selectedPatientNotes = [];
        _selectedDoctorIssuedPatientNotes = [];
        _initializeAllPatientsNotesLazy = new Lazy<Task>(InitializePatientsNotes);
    }

    public async Task LoadPatientsNotes(){
        await _initializeAllPatientsNotesLazy.Value;
    }

    public async Task LoadSelectedPatientNotes(){
        var foundPatientNotes =
            await _unitOfWork.PatientNoteRepository.GetIssuedPatientNotesByPatientOrDoctorId(SelectedPatientId,
                PersonType.Patient);
        _selectedPatientNotes.AddRange(foundPatientNotes);
    }

    public async Task LoadSelectedDoctorIssuedPatientNotes(){
        var foundPatientNotes =await _unitOfWork.PatientNoteRepository.GetIssuedPatientNotesByPatientOrDoctorId(SelectedDoctorId,
            PersonType.Doctor);
        _selectedDoctorIssuedPatientNotes.AddRange(foundPatientNotes);
    }
    
    public async Task LoadSelectedPatientNote(){
        var loadedPatientNote = await _unitOfWork.PatientNoteRepository.GetPatientNoteDetailsAsync(SelectedPatientNoteId).ConfigureAwait(false);
        if (loadedPatientNote != null) SelectedPatientNote = loadedPatientNote;
    }
    
    public async Task CreatePatientNote(SavePatientNoteRequest patientNoteRequest){
        var savedPatientNoteDto = await _unitOfWork.PatientNoteRepository.SavePatientNoteAsync(patientNoteRequest);
        _allPatientsNotesDto.Add(savedPatientNoteDto);

        OnPatientNoteCreated(savedPatientNoteDto);
    }
    
    private async Task InitializePatientsNotes(){
        var loadedPatientsNotes = await _unitOfWork.PatientNoteRepository.GetAllPatientsNotesAsync();
        
        _allPatientsNotesDto.Clear();
        _allPatientsNotesDto.AddRange(loadedPatientsNotes);
    }
    
    private void OnPatientNoteCreated(PatientNoteDto savedPatientNoteDto){
        PatientNoteCreated?.Invoke(savedPatientNoteDto);
    }
}