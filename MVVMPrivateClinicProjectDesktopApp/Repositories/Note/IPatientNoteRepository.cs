using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Note;

public interface IPatientNoteRepository {
    Task<IEnumerable<PatientNoteDto>> GetAllPatientsNotesAsync();
}