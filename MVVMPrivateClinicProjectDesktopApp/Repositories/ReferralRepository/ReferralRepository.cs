using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Base;
using MVVMPrivateClinicProjectDesktopApp.Repositories.DiseaseRepository;
using MVVMPrivateClinicProjectDesktopApp.Repositories.DoctorRepository;
using MVVMPrivateClinicProjectDesktopApp.Repositories.PatientRepository;
using MVVMPrivateClinicProjectDesktopApp.Repositories.ReferralTestRepository;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.ReferralRepository;

public class ReferralRepository(
    DbContextFactory dbContextFactory,
    IMapper mapper,
    IPatientRepository patientRepository,
    IDoctorRepository doctorRepository,
    IDiseaseRepository diseaseRepository,
    IReferralTestRepository referralTestRepository
    ) : BaseRepository<Referral, ReferralDto>(dbContextFactory, mapper), IReferralRepository {
    private readonly DbContextFactory _dbContextFactory = dbContextFactory;
    private readonly IMapper _mapper = mapper;

    public async Task<ReferralDto> SaveReferralAsync(SaveReferralRequest referralRequest){
        await using var context = _dbContextFactory.CreateDbContext();
        
        var dateIssued = DateTime.Now;

        var referral = new Referral {
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
        
        return _mapper.Map<Referral, ReferralDto>(referral);
    }

    public async Task<ReferralDetailsDto?> GetReferralDetailsByIdAsync(int id){
        await using var context = _dbContextFactory.CreateDbContext();

        var foundReferral = await context.Referrals
            .Where(r => r.Id == id)
            .ProjectTo<ReferralDetailsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (foundReferral is null) return null;

        var foundPatient = await patientRepository.GetPatientDetailsAsync(foundReferral.IdPatient);
        var foundDoctor = await doctorRepository.GetDoctorDetailsAsync(foundReferral.IdDoctor);
        var foundDisease = await diseaseRepository.GetDiseaseByIdAsync(foundReferral.IdDisease);
        var foundReferralTest = await referralTestRepository.GetReferralTestByIdAsync(foundReferral.IdReferralTest);

        if (foundPatient != null) foundReferral.PatientDetailsDto = foundPatient;
        if (foundDoctor != null) foundReferral.DoctorDtoBase = foundDoctor;
        if (foundDisease != null) foundReferral.DiseaseDetailsDto = foundDisease;
        if (foundReferralTest != null) foundReferral.ReferralTestDetailsDto = foundReferralTest;

        return foundReferral;
    }

    public async Task<IEnumerable<ReferralDto>> GetIssuedReferralsByPatientIdOrDoctorId(int personId, PersonType personType){
        await using var context = _dbContextFactory.CreateDbContext();

        return await context.Referrals
            .Where(referral => personType.Equals(PersonType.Patient) ? referral.IdPatient == personId : referral.IdDoctor == personId)
            .ProjectTo<ReferralDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<ReferralDto>> GetAllReferralsDtoAsync(){
        var foundReferralsDto = await GetAllEntitiesAsync();
        var allReferralsDtoAsync = foundReferralsDto as ReferralDto[] ?? foundReferralsDto.ToArray();

        foreach (var referralDto in allReferralsDtoAsync) {
            var foundPatient = await patientRepository.GetPatientByIdAsync(referralDto.IdPatient);
            var foundDoctor = await doctorRepository.GetDoctorByIdAsync(referralDto.IdDoctor);
            var foundDisease = await diseaseRepository.GetDiseaseByIdAsync(referralDto.IdDisease);

            if (foundPatient?.PatientCode != null) referralDto.PatientCode = foundPatient.PatientCode;
            if (foundDoctor?.DoctorCode != null) referralDto.DoctorCode = foundDoctor.DoctorCode;
            if (foundDisease?.DiseaseCode != null) referralDto.DiseaseCode = foundDisease.DiseaseCode;
        }
        
        return allReferralsDtoAsync;
    }

    public async Task<int> CountIssuedReferralsAsync(){
        return await CountEntitiesAsync();
    }

    public async Task<int> CountIssuedReferralsByDoctorIdAsync(int doctorId){
        await using var context = _dbContextFactory.CreateDbContext();
        return await context.Referrals
            .Where(referral => referral.IdDoctor == doctorId)
            .CountAsync();
    }

    public async Task<int> CountReferralTestUsesAsync(int referralTestId){
        await using var context = _dbContextFactory.CreateDbContext();
        
        return await context.Referrals
            .Where(referral => referral.IdReferralTest == referralTestId)
            .CountAsync();
    }
}