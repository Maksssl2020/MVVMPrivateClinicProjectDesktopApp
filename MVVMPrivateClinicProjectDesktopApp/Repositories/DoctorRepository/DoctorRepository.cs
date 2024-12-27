using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Base;
using MVVMPrivateClinicProjectDesktopApp.Repositories.DoctorSpecializationRepository;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.DoctorRepository;

public class DoctorRepository(
    DbContextFactory dbContextFactory,
    IMapper mapper,
    IDoctorSpecializationRepository doctorSpecializationRepository
    ) : BaseRepository<Doctor, DoctorDto>(dbContextFactory, mapper), IDoctorRepository {
    private readonly DbContextFactory _dbContextFactory = dbContextFactory;
    private readonly IMapper _mapper = mapper;

    public async Task<DoctorDto> SaveDoctorAsync(SaveDoctorRequest doctorRequest){
        await using var context = _dbContextFactory.CreateDbContext();

        var doctor = new Doctor {
            FirstName = doctorRequest.FirstName,
            LastName = doctorRequest.LastName,
            PhoneNumber = doctorRequest.PhoneNumber,
            IdDoctorSpecialization = doctorRequest.DoctorSpecializationId,
        };
        
        await context.Doctors.AddAsync(doctor);
        await context.SaveChangesAsync();


        var doctorDto = _mapper.Map<DoctorDto>(doctor);
        AppendDoctorSpecialization(doctorDto, await GetAllDoctorSpecializationsAsList());
        
        return doctorDto;
    }

    public async Task<IEnumerable<DoctorDto>> GetAllDoctors(){
        var doctors = await GetAllEntitiesAsync();
        var allDoctorSpecializationsAsList = await GetAllDoctorSpecializationsAsList();

        List<DoctorDto> doctorsDto = [];
        
        foreach (var doctorDto in doctors.Select(_mapper.Map<DoctorDto>)) {
            AppendDoctorSpecialization(doctorDto, allDoctorSpecializationsAsList);
            doctorsDto.Add(doctorDto);
        }
        
        return doctorsDto;
    }

    public async Task<IEnumerable<DoctorDto>> GetAllFamilyMedicineDoctors(){
        await using var context = _dbContextFactory.CreateDbContext();
        var doctors = await context.Doctors.ToListAsync();
        var specializations = await doctorSpecializationRepository.GetAllDoctorSpecializations();

        return doctors
            .Where(doctor => {
                var specialization = specializations.FirstOrDefault(s => s.Id == doctor.IdDoctorSpecialization);

                return specialization is { Name: "Family Medicine" };
            })
            .Select(doctor => {
                var doctorDto = _mapper.Map<DoctorDto>(doctor);
                var doctorDtoDoctorSpecialization =
                    specializations.FirstOrDefault(s => s.Id == doctor.IdDoctorSpecialization)?.Name;

                if (doctorDtoDoctorSpecialization != null)
                    doctorDto.DoctorSpecialization =
                        doctorDtoDoctorSpecialization;

                return doctorDto;
            })
            .ToList();
    }

    public async Task<IEnumerable<DoctorDto>> GetMostPopularDoctors(int size){
        await using var context = _dbContextFactory.CreateDbContext();
        var allDoctorSpecializationsAsList = await GetAllDoctorSpecializationsAsList();
        List<DoctorDto> doctorsDto = [];
        
        var foundDoctors = await context.Doctors
            .Include(doctor => doctor.Appointments)
            .OrderByDescending(doctor => doctor.Appointments.Count)
            .Take(size)
            .ToListAsync();

        foreach (var doctorDto in foundDoctors.Select(_mapper.Map<DoctorDto>)) {
            AppendDoctorSpecialization(doctorDto, allDoctorSpecializationsAsList);
            doctorsDto.Add(doctorDto);
        }
        
        return doctorsDto;
    }

    public async Task<DoctorDto?> GetDoctorByIdAsync(int doctorId){
        await using var context = _dbContextFactory.CreateDbContext();
        var foundDoctor = await GetEntityByIdAsync(doctorId);
        
        if (foundDoctor == null) return null;
        AppendDoctorSpecialization(foundDoctor, await GetAllDoctorSpecializationsAsList());

        return foundDoctor;
    }

    public async Task<DoctorDtoBase?> GetDoctorDetailsAsync(int doctorId){
        await using var context = _dbContextFactory.CreateDbContext();

        var foundDoctor = await context.Doctors
            .ProjectTo<DoctorDtoBase>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(doctor => doctor.Id == doctorId);
        
        if (foundDoctor == null) return null;
        
        AppendDoctorSpecialization(foundDoctor, await GetAllDoctorSpecializationsAsList());
        return foundDoctor;
    }

    public async Task<int> CountDoctorsAsync(){
        return await CountEntitiesAsync();
    }

    private async Task<List<DoctorSpecializationDto>> GetAllDoctorSpecializationsAsList(){
        var allDoctorSpecializations = await doctorSpecializationRepository.GetAllDoctorSpecializations();
        return allDoctorSpecializations.ToList();
    }
    
    private static void AppendDoctorSpecialization(DoctorDtoBase doctorDto, IEnumerable<DoctorSpecializationDto> allDoctorSpecializations){
        var doctorSpecializationDto = allDoctorSpecializations.FirstOrDefault(specialization => specialization.Id == doctorDto.IdDoctorSpecialization);
        if (doctorSpecializationDto?.Name != null) doctorDto.DoctorSpecialization = doctorSpecializationDto.Name;
    }
}