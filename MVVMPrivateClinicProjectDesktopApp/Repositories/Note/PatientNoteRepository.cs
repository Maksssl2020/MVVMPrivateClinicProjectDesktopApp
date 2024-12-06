using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Doctor;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Note;

public class PatientNoteRepository(DbContextFactory dbContextFactory, IMapper mapper, IPatientRepository patientRepository, IDoctorRepository doctorRepository) : IPatientNoteRepository {
    public async Task<IEnumerable<PatientNoteDto>> GetAllPatientsNotesAsync(){
        await using var context = dbContextFactory.CreateDbContext();

        var foundPatientsNotes = await context.PatientNotes
            .ProjectTo<PatientNoteDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        foreach (var patientNote in foundPatientsNotes) {
            var foundPatient = await patientRepository.GetPatientByIdAsync(patientNote.IdPatient);
            var foundDoctor = await doctorRepository.GetDoctorByIdAsync(patientNote.IdDoctor);

            if (foundPatient?.PatientCode != null) patientNote.PatientCode = foundPatient.PatientCode;
            if (foundDoctor?.DoctorCode != null) patientNote.DoctorCode = foundDoctor.DoctorCode;
        }
        
        return foundPatientsNotes;
    }
}