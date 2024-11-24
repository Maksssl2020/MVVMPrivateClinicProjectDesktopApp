using AutoMapper;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Models.Entities;

namespace MVVMPrivateClinicProjectDesktopApp.Helpers;

public class MapperProfiles : Profile {
    public MapperProfiles(){
        CreateMap<Doctor, DoctorDTO>()
            .ForMember(dest => dest.DoctorSpecialization, 
                opt => opt.Ignore());
    }
}