using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Note;

public interface IPatientNoteRepository {
    Task<PatientNoteDto> SavePatientNoteAsync(SavePatientNoteRequest patientNoteRequest);
    Task<PatientNoteDetailsDto?> GetPatientNoteDetailsAsync(int patientNoteId);
    Task<IEnumerable<PatientNoteDto>> GetAllPatientsNotesAsync();
    Task<IEnumerable<PatientNoteWithDoctorDataDto>> GetAllPatientNotesByPatientIdAsync(int patientId);
    Task<int> CountIssuedPatientNotesByDoctorIdAsync(int doctorId);
}