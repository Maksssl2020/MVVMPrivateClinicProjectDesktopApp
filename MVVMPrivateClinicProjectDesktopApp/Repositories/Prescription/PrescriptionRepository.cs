using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Doctor;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Prescription;

public class PrescriptionRepository(DbContextFactory dbContextFactory, IMapper mapper, IPatientRepository patientRepository, IDoctorRepository doctorRepository) : IPrescriptionRepository {
    public async Task<IEnumerable<PrescriptionDto>> GetAllPrescriptionsDtoAsync(){
        await using var context = dbContextFactory.CreateDbContext();

        var foundPrescriptionsDto = await context.Prescriptions
            .ProjectTo<PrescriptionDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        foreach (var prescriptionDto in foundPrescriptionsDto) {
            var foundPatient = await patientRepository.GetPatientByIdAsync(prescriptionDto.IdPatient);
            var foundDoctor = await doctorRepository.GetDoctorByIdAsync(prescriptionDto.IdDoctor);

            if (foundPatient?.PatientCode != null) prescriptionDto.PatientCode = foundPatient.PatientCode;
            if (foundDoctor != null) prescriptionDto.DoctorCode = foundDoctor.DoctorCode;
        }
        
        return foundPrescriptionsDto;
    }
}