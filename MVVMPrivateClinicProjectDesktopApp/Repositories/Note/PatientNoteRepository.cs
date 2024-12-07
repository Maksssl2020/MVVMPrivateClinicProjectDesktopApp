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

        var patientNote = new PatientNote {
            DateIsuued = patientNoteRequest.DateIssued,
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

    private async Task AssignPatientCodeAndDoctorCodeToPatientNoteDto(PatientNoteDto patientNote){
        var foundPatient = await patientRepository.GetPatientByIdAsync(patientNote.IdPatient);
        var foundDoctor = await doctorRepository.GetDoctorByIdAsync(patientNote.IdDoctor);

        if (foundPatient?.PatientCode != null) patientNote.PatientCode = foundPatient.PatientCode;
        if (foundDoctor?.DoctorCode != null) patientNote.DoctorCode = foundDoctor.DoctorCode;  
    }
}