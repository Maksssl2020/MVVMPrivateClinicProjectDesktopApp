using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Doctor;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Note;

public class PatientNoteRepository(DbContextFactory dbContextFactory, IMapper mapper, IPatientRepository patientRepository, IDoctorRepository doctorRepository) : IPatientNoteRepository {
    public async Task<PatientNoteDto> SavePatientNoteAsync(SavePatientNoteRequest patientNoteRequest){
        await using var context = dbContextFactory.CreateDbContext();

        var dateIssued = DateTime.Now;
        
        var patientNote = new PatientNote {
            DateIsuued = dateIssued,
            Description = patientNoteRequest.Description,
            IdDoctor = patientNoteRequest.IdDoctor,
            IdPatient = patientNoteRequest.IdPatient,
        };
        
        await context.PatientNotes.AddAsync(patientNote);
        await context.SaveChangesAsync();

        var savedPatientNoteDto = mapper.Map<PatientNoteDto>(patientNote);
        await AssignPatientCodeAndDoctorCodeToPatientNoteDto(savedPatientNoteDto);
        
        return savedPatientNoteDto;
    }

    public async Task<PatientNoteDetailsDto?> GetPatientNoteDetailsAsync(int patientNoteId){
        await using var context = dbContextFactory.CreateDbContext();
        
        var foundPatientNote = await context.PatientNotes
            .Where(p => p.Id == patientNoteId)
            .ProjectTo<PatientNoteDetailsDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (foundPatientNote == null) return null;
        
        var foundDoctor =
            await doctorRepository.GetDoctorDetailsAsync(foundPatientNote.IdDoctor);
        var foundPatient = await patientRepository.GetPatientDetailsAsync(foundPatientNote.IdPatient);

        if (foundDoctor != null) foundPatientNote.DoctorDtoBase = foundDoctor;
        if (foundPatient != null) foundPatientNote.PatientDetailsDto = foundPatient;

        return foundPatientNote;
    }

    public async Task<IEnumerable<PatientNoteDto>> GetAllPatientsNotesAsync(){
        await using var context = dbContextFactory.CreateDbContext();

        var foundPatientsNotes = await context.PatientNotes
            .ProjectTo<PatientNoteDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        foreach (var patientNote in foundPatientsNotes) {
            await AssignPatientCodeAndDoctorCodeToPatientNoteDto(patientNote);
        }
        
        return foundPatientsNotes;
    }

    public async Task<IEnumerable<PatientNoteWithDoctorDataDto>> GetIssuedPatientNotesByPatientOrDoctorId(int personId, PersonType personType){
        await using var context = dbContextFactory.CreateDbContext();

        var foundPatientNotes = await context.PatientNotes
            .Where(p => personType.Equals(PersonType.Patient) ? p.IdPatient == personId : p.IdDoctor == personId)
            .ProjectTo<PatientNoteWithDoctorDataDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        foreach (var patientNote in foundPatientNotes) {
            await AssignDoctorDataToPatientNoteWithDoctorDataDtoAndPatientCode(patientNote);
        }
        
        return foundPatientNotes;
    }

    public async Task<int> CountIssuedPatientNotesByDoctorIdAsync(int doctorId){
        await using var context = dbContextFactory.CreateDbContext();
        return await context.PatientNotes
            .Where(p => p.IdDoctor == doctorId)
            .CountAsync();
    }

    private async Task AssignPatientCodeAndDoctorCodeToPatientNoteDto(PatientNoteDto patientNote){
        var foundPatient = await patientRepository.GetPatientByIdAsync(patientNote.IdPatient);
        var foundDoctor = await doctorRepository.GetDoctorByIdAsync(patientNote.IdDoctor);

        if (foundPatient?.PatientCode != null) patientNote.PatientCode = foundPatient.PatientCode;
        if (foundDoctor?.DoctorCode != null) patientNote.DoctorCode = foundDoctor.DoctorCode;  
    }

    private async Task AssignDoctorDataToPatientNoteWithDoctorDataDtoAndPatientCode(
        PatientNoteWithDoctorDataDto patientNoteWithDoctorDataDto){
        var foundPatient = await patientRepository.GetPatientByIdAsync(patientNoteWithDoctorDataDto.IdPatient);
        var foundDoctor = await doctorRepository.GetDoctorByIdAsync(patientNoteWithDoctorDataDto.IdDoctor);

        if (foundDoctor != null) {
            patientNoteWithDoctorDataDto.DoctorDto = foundDoctor;
        }

        if (foundPatient != null) {
            patientNoteWithDoctorDataDto.PatientCode = foundPatient.PatientCode;
        }
    }
}