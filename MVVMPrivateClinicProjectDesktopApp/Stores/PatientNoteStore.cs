using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class PatientNoteStore {
    private readonly IUnitOfWork _unitOfWork;

    private readonly List<PatientNoteDto> _allPatientsNotesDto;
    private readonly Lazy<Task> _initializeAllPatientsNotesLazy;
    private readonly List<PatientNoteWithDoctorDataDto> _selectedPatientNotes;
    private readonly Lazy<Task> _initializeSelectedPatientNotesLazy;

    public IEnumerable<PatientNoteDto> AllPatientsNotesDto => _allPatientsNotesDto;
    public IEnumerable<PatientNoteWithDoctorDataDto> SelectedPatientNotes => _selectedPatientNotes;

    public int SelectedPatientId { get; set; }

    private int _selectedPatientNoteId;

    public int SelectedPatientNoteId {
        get => _selectedPatientNoteId;
        set {
            _selectedPatientNoteId = value;
            SelectedPatientNote = null!;
        }
    }

    public PatientNoteDetailsDto SelectedPatientNote { get; set; } = null!;

    public event Action<PatientNoteDto>? PatientNoteCreated;
    public PatientNoteStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;

        _allPatientsNotesDto = [];
        _selectedPatientNotes = [];
        _initializeAllPatientsNotesLazy = new Lazy<Task>(InitializePatientsNotes);
        _initializeSelectedPatientNotesLazy = new Lazy<Task>(InitializeSelectedPatientNotes);
    }

    public async Task LoadPatientsNotes(){
        await _initializeAllPatientsNotesLazy.Value;
    }

    public async Task LoadSelectedPatientNotes(){
        await _initializeSelectedPatientNotesLazy.Value;
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

    private async Task InitializeSelectedPatientNotes(){
        var loadedSelectedPatientNotes = await _unitOfWork.PatientNoteRepository.GetAllPatientNotesByPatientIdAsync(SelectedPatientId);
        
        _selectedPatientNotes.Clear();
        _selectedPatientNotes.AddRange(loadedSelectedPatientNotes);
    }
    
    private void OnPatientNoteCreated(PatientNoteDto savedPatientNoteDto){
        PatientNoteCreated?.Invoke(savedPatientNoteDto);
    }
}