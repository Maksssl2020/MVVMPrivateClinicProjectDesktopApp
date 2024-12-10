using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Disease;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Doctor;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Referral;

public class ReferralRepository(
    DbContextFactory dbContextFactory,
    IMapper mapper,
    IPatientRepository patientRepository,
    IDoctorRepository doctorRepository,
    IDiseaseRepository diseaseRepository
    ) : IReferralRepository {
    public async Task<ReferralDto> SaveReferral(SaveReferralRequest referralRequest){
        await using var context = dbContextFactory.CreateDbContext();
        
        var dateIssued = DateTime.Now;

        var referral = new Models.Entities.Referral {
            DateIssued = dateIssued,
            Description = referralRequest.Description,
            Name = referralRequest.Name,
            IdPatient = referralRequest.PatientId,
            IdDoctor = referralRequest.DoctorId,
            IdDisease = referralRequest.DiseaseId ?? null,
            IdReferralTest = referralRequest.ReferralTestId,
        };
        
        await context.AddAsync(referral);
        await context.SaveChangesAsync();
        
        return mapper.Map<Models.Entities.Referral, ReferralDto>(referral);
    }

    public async Task<IEnumerable<ReferralDto>> GetPatientAllReferrals(int patientId){
        await using var context = dbContextFactory.CreateDbContext();

        return await context.Referrals
            .Where(referral => referral.IdPatient == patientId)
            .ProjectTo<ReferralDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<ReferralDto>> GetAllReferralsDtoAsync(){
        await using var context = dbContextFactory.CreateDbContext();

        var foundReferralsDto = await context.Referrals
            .ProjectTo<ReferralDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        foreach (var referralDto in foundReferralsDto) {
            var foundPatient = await patientRepository.GetPatientByIdAsync(referralDto.IdPatient);
            var foundDoctor = await doctorRepository.GetDoctorByIdAsync(referralDto.IdDoctor);
            var foundDisease = await diseaseRepository.GetDiseaseByIdAsync(referralDto.IdDisease);

            if (foundPatient?.PatientCode != null) referralDto.PatientCode = foundPatient.PatientCode;
            if (foundDoctor?.DoctorCode != null) referralDto.DoctorCode = foundDoctor.DoctorCode;
            if (foundDisease?.DiseaseCode != null) referralDto.DiseaseCode = foundDisease.DiseaseCode;
        }
        
        return foundReferralsDto;
    }
}