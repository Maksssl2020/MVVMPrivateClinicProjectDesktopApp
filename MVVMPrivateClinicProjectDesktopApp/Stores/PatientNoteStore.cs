using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class PatientNoteStore {
    private readonly IUnitOfWork _unitOfWork;

    private readonly List<PatientNoteDto> _patientsNotesDto;
    private readonly Lazy<Task> _initializeLazy;

    public IEnumerable<PatientNoteDto> PatientsNotesDto => _patientsNotesDto;

    public PatientNoteStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;

        _patientsNotesDto = [];
        _initializeLazy = new Lazy<Task>(InitializePatientsNotes);
    }

    public async Task LoadPatientsNotes(){
        await _initializeLazy.Value;
    }

    private async Task InitializePatientsNotes(){
        var loadedPatientsNotes = await _unitOfWork.PatientNoteRepository.GetAllPatientsNotesAsync();
        
        _patientsNotesDto.Clear();
        _patientsNotesDto.AddRange(loadedPatientsNotes);
    }
}