using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.DoctorSpecialization;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Doctor;

public class DoctorRepository(DbContextFactory dbContextFactory, IMapper mapper, IDoctorSpecializationRepository doctorSpecializationRepository) : IDoctorRepository {
    public async Task<IEnumerable<DoctorDto>> GetAllDoctors(){
        await using var context = dbContextFactory.CreateDbContext();
        var doctors = await context.Doctors.ToListAsync();
        var specializations = await doctorSpecializationRepository.GetAllDoctorSpecializations();

        return doctors.Select(doctor => {
            var doctorDto = mapper.Map<DoctorDto>(doctor);
            var doctorSpecialization = specializations.FirstOrDefault(s => s.Id == doctor.IdDoctorSpecialization);
            doctorDto.DoctorSpecialization = doctorSpecialization?.Name;
            
            return doctorDto;
        }).ToList();
    }

    public async Task<IEnumerable<DoctorDto>> GetAllFamilyMedicineDoctors(){
        await using var context = dbContextFactory.CreateDbContext();
        var doctors = await context.Doctors.ToListAsync();
        var specializations = await doctorSpecializationRepository.GetAllDoctorSpecializations();

        return doctors
            .Where(doctor => {
                var specialization = specializations.FirstOrDefault(s => s.Id == doctor.IdDoctorSpecialization);

                return specialization is { Name: "Family Medicine" };
            })
            .Select(doctor => {
                var doctorDto = mapper.Map<DoctorDto>(doctor);
                var doctorDtoDoctorSpecialization =
                    specializations.FirstOrDefault(s => s.Id == doctor.IdDoctorSpecialization)?.Name;
                
                doctorDto.DoctorSpecialization =
                    doctorDtoDoctorSpecialization;

                return doctorDto;
            })
            .ToList();
    }

    public async Task<DoctorDto?> GetDoctorByIdAsync(int id){
        await using var context = dbContextFactory.CreateDbContext();
        var foundDoctor = await context.Doctors.FirstOrDefaultAsync(doctor => doctor.Id == id);
        if (foundDoctor == null) return null;
        var foundSpecialization = await doctorSpecializationRepository.GetDoctorSpecializationById(foundDoctor.Id);
        
        var doctorDto = mapper.Map<DoctorDto>(foundDoctor);
        doctorDto.DoctorSpecialization = foundSpecialization?.Name;
        
        return doctorDto;
    }
}