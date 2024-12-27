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
        CreateMap<Medicine, MedicineDetailsDto>()
            .ForMember(dest => dest.TotalUses, opt => opt.Ignore());

        CreateMap<Prescription, PrescriptionDto>()
            .ForMember(dest => dest.PatientCode, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorCode, opt => opt.Ignore());

        CreateMap<Referral, ReferralDto>()
            .ForMember(dest => dest.PatientCode, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorCode, opt => opt.Ignore())
            .ForMember(dest => dest.DiseaseCode, opt => opt.Ignore());
        CreateMap<Referral, ReferralDetailsDto>()
            .ForMember(dest => dest.PatientDetailsDto, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorDtoBase, opt => opt.Ignore())
            .ForMember(dest => dest.DiseaseDetailsDto, opt => opt.Ignore())
            .ForMember(dest => dest.ReferralTestDetailsDto, opt => opt.Ignore());

        CreateMap<Invoice, InvoiceDto>()
            .ForMember(dest => dest.PatientCode, opt => opt.Ignore());

        CreateMap<Invoice, InvoiceDetailsDto>()
            .ForMember(dest => dest.PatientDetailsDto, opt => opt.Ignore())
            .ForMember(dest => dest.PricingDto, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore());
        
        CreateMap<Pricing, PricingDto>();
        CreateMap<Pricing, TopPricingDto>()
            .ForMember(dest => dest.TotalUseAmount, opt => opt.MapFrom(src => src.Appointments.Count))
            .ForMember(dest => dest.Position, opt => opt.Ignore());
        CreateMap<Pricing, PricingDetailsDto>()
            .ForMember(dest => dest.TotalUses, opt => opt.MapFrom(src => src.Appointments.Count));
        
        CreateMap<PatientNote, PatientNoteDto>()
            .ForMember(dest => dest.PatientCode, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorCode, opt => opt.Ignore());
        CreateMap<PatientNote, PatientNoteWithDoctorDataDto>()
            .ForMember(dest => dest.PatientCode, opt => opt.Ignore());
        CreateMap<PatientNote, PatientNoteDetailsDto>()
            .ForMember(dest => dest.PatientDetailsDto, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorDtoBase, opt => opt.Ignore());
        
        CreateMap<Disease, DiseaseDto>();
        CreateMap<Disease, DiseaseDetailsDto>()
            .ForMember(dest => dest.TotalDiagnoses, opt => opt.Ignore());
        
        CreateMap<DoctorSpecialization, DoctorSpecializationDto>();
        
        CreateMap<ReferralTest, ReferralTestDto>();
        CreateMap<ReferralTest, ReferralTestDetailsDto>()
            .ForMember(dest => dest.TotalUses, opt => opt.Ignore());
        
        CreateMap<Diagnosis, DiagnosisDto>()
            .ForMember(dest => dest.PatientCode, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorCode, opt => opt.Ignore())
            .ForMember(dest => dest.DiseaseCode, opt => opt.Ignore());

        CreateMap<Doctor, DoctorDtoBase>();
        CreateMap<Patient, PatientDetailsDto>();
        CreateMap<Patient, PatientDto>();
        CreateMap<Prescription, PrescriptionDetailsDto>()
            .ForMember(dest => dest.MedicinesDto, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorDtoBase, opt => opt.Ignore())
            .ForMember(dest => dest.PatientDetailsDto, opt => opt.Ignore());
    }
}