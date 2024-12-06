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