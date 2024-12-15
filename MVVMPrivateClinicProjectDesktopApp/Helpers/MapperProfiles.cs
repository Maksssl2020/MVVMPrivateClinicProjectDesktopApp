using AutoMapper;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Helpers;

public class MapperProfiles : Profile {
    public MapperProfiles(){
        CreateMap<Doctor, DoctorDto>()
            .ForMember(dest => dest.DoctorSpecialization,
                opt => opt.Ignore())
            .ForMember(dest => dest.AmountOfAppointments, opt => opt.MapFrom(src => src.Appointments.Count));

        CreateMap<Appointment, AppointmentDto>();
        CreateMap<AppointmentDate, AppointmentDateDto>()
            .ForMember(dest => dest.AppointmentDate,
                opt => opt.Ignore())
            .ForMember(dest => dest.AppointmentTime,
                opt => opt.Ignore());
        
        CreateMap<Medicine, MedicineDto>();

        CreateMap<Prescription, PrescriptionDto>()
            .ForMember(dest => dest.PatientCode, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorCode, opt => opt.Ignore());

        CreateMap<Referral, ReferralDto>()
            .ForMember(dest => dest.PatientCode, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorCode, opt => opt.Ignore())
            .ForMember(dest => dest.DiseaseCode, opt => opt.Ignore());
        CreateMap<Referral, ReferralDetailsDto>()
            .ForMember(dest => dest.PatientDetailsDto, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorDetailsDto, opt => opt.Ignore())
            .ForMember(dest => dest.DiseaseDetailsDto, opt => opt.Ignore())
            .ForMember(dest => dest.ReferralTestDetailsDto, opt => opt.Ignore());
        
        CreateMap<Invoice, InvoiceDto>()
            .ForMember(dest => dest.PatientCode, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore());

        CreateMap<Pricing, PricingDto>();
        CreateMap<PatientNote, PatientNoteDto>()
            .ForMember(dest => dest.PatientCode, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorCode, opt => opt.Ignore());
        CreateMap<PatientNote, PatientNoteWithDoctorDataDto>()
            .ForMember(dest => dest.DoctorCode, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorFirstName, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorLastName, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorSpecialization, opt => opt.Ignore());
        
        CreateMap<PatientNote, PatientNoteDetailsDto>()
            .ForMember(dest => dest.PatientDetailsDto, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorDetailsDto, opt => opt.Ignore());
        
        CreateMap<Disease, DiseaseDto>();
        CreateMap<DoctorSpecialization, DoctorSpecializationDto>();
        CreateMap<ReferralTest, ReferralTestDto>();
        CreateMap<Diagnosis, DiagnosisDto>()
            .ForMember(dest => dest.PatientCode, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorCode, opt => opt.Ignore())
            .ForMember(dest => dest.DiseaseCode, opt => opt.Ignore());

        CreateMap<Doctor, DoctorDetailsDto>();
        CreateMap<Patient, PatientDetailsDto>();
        CreateMap<Patient, PatientDto>();
        CreateMap<Prescription, PrescriptionDetailsDto>()
            .ForMember(dest => dest.MedicinesDto, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorDetailsDto, opt => opt.Ignore())
            .ForMember(dest => dest.PatientDetailsCto, opt => opt.Ignore());
    }
}