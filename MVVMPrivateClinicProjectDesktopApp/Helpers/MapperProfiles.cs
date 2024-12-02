using AutoMapper;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Helpers;

public class MapperProfiles : Profile {
    public MapperProfiles(){
        CreateMap<Doctor, DoctorDto>()
            .ForMember(dest => dest.DoctorSpecialization, 
                opt => opt.Ignore());

        CreateMap<Appointment, AppointmentDto>();
        CreateMap<Medicine, MedicineDto>();
    }
}