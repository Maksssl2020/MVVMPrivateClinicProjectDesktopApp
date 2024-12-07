using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Note;

public interface IPatientNoteRepository {
    Task<PatientNoteDto> SavePatientNoteAsync(SavePatientNoteRequest patientNoteRequest);
    Task<IEnumerable<PatientNoteDto>> GetAllPatientsNotesAsync();
    Task<IEnumerable<PatientNoteWithDoctorDataDto>> GetAllPatientNotesByPatientIdAsync(int patientId);
}