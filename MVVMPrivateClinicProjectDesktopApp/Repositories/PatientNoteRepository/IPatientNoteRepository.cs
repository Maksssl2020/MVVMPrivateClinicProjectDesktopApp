using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Base;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.PatientNoteRepository;

public interface IPatientNoteRepository : IBaseRepository<PatientNoteDto> {
    Task<PatientNoteDto> SavePatientNoteAsync(SavePatientNoteRequest patientNoteRequest);
    Task<PatientNoteDetailsDto?> GetPatientNoteDetailsAsync(int patientNoteId);
    Task<IEnumerable<PatientNoteDto>> GetAllPatientsNotesAsync();
    Task<IEnumerable<PatientNoteWithDoctorDataDto>> GetIssuedPatientNotesByPatientOrDoctorId(int personId, PersonType personType);
    Task<int> CountIssuedPatientNotesByDoctorIdAsync(int doctorId);
}