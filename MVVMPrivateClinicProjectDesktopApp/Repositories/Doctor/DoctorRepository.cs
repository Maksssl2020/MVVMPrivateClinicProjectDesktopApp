using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.DoctorSpecialization;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Doctor;

public class DoctorRepository(DbContextFactory dbContextFactory, IMapper mapper, IDoctorSpecializationRepository doctorSpecializationRepository) : IDoctorRepository {
    public async Task<DoctorDto> SaveDoctorAsync(SaveDoctorRequest doctorRequest){
        await using var context = dbContextFactory.CreateDbContext();

        var doctor = new Models.Entities.Doctor {
            FirstName = doctorRequest.FirstName,
            LastName = doctorRequest.LastName,
            PhoneNumber = doctorRequest.PhoneNumber,
            IdDoctorSpecialization = doctorRequest.DoctorSpecializationId,
        };
        
        await context.Doctors.AddAsync(doctor);
        await context.SaveChangesAsync();


        var doctorDto = mapper.Map<DoctorDto>(doctor);
        await AppendDoctorSpecialization(doctorDto);
        
        return doctorDto;
    }

    public async Task<IEnumerable<DoctorDto>> GetAllDoctors(){
        await using var context = dbContextFactory.CreateDbContext();
        var doctors = await context.Doctors.ToListAsync();
        List<DoctorDto> doctorsDto = [];
        
        foreach (var doctorDto in doctors.Select(mapper.Map<DoctorDto>)) {
            await AppendDoctorSpecialization(doctorDto);
            doctorsDto.Add(doctorDto);
        }
        
        return doctorsDto;
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

    public async Task<IEnumerable<DoctorDto>> GetMostPopularDoctors(int size){
        await using var context = dbContextFactory.CreateDbContext();

        List<DoctorDto> doctorsDto = [];
        
        var foundDoctors = await context.Doctors
            .OrderByDescending(doctor => doctor.Appointments.Count)
            .Take(size)
            .ToListAsync();

        foreach (var doctorDto in foundDoctors.Select(mapper.Map<DoctorDto>)) {
            await AppendDoctorSpecialization(doctorDto);
            doctorsDto.Add(doctorDto);
        }
        
        return doctorsDto;
    }

    public async Task<DoctorDto?> GetDoctorByIdAsync(int id){
        await using var context = dbContextFactory.CreateDbContext();
        var foundDoctor = await context.Doctors.FirstOrDefaultAsync(doctor => doctor.Id == id);
        
        if (foundDoctor == null) return null;
        var doctorDto = mapper.Map<DoctorDto>(foundDoctor);

        await AppendDoctorSpecialization(doctorDto);

        return doctorDto;
    }

    public async Task<DoctorDetailsDto?> GetDoctorDetailsAsync(int doctorId){
        await using var context = dbContextFactory.CreateDbContext();

        return await context.Doctors
            .ProjectTo<DoctorDetailsDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(doctor => doctor.Id == doctorId);
    }

    private async Task AppendDoctorSpecialization(DoctorDto doctorDto){ 
        var foundSpecialization =  await doctorSpecializationRepository.GetDoctorSpecializationById(doctorDto.Id);
        doctorDto.DoctorSpecialization = foundSpecialization?.Name;
    }
}