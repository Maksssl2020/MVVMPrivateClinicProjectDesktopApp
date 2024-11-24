using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.DoctorSpecialization;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Doctor;

public class DoctorRepository(IMapper mapper, IDoctorSpecializationRepository doctorSpecializationRepository) : RepositoryBase, IDoctorRepository {
    public async Task<IEnumerable<DoctorDTO>> GetAllDoctors(){
        var doctors = await DbContext.Doctors.ToListAsync();
        var specializations = await doctorSpecializationRepository.GetAllDoctorSpecializations();

        return doctors.Select(doctor => {
            var doctorDto = mapper.Map<DoctorDTO>(doctor);
            var doctorSpecialization = specializations.FirstOrDefault(s => s.Id == doctor.IdDoctorSpecialization);
            doctorDto.DoctorSpecialization = doctorSpecialization?.Name;
            
            return doctorDto;
        }).ToList();
    }
}