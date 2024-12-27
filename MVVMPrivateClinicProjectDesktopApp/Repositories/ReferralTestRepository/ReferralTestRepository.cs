using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Base;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.ReferralTestRepository;

public class ReferralTestRepository(DbContextFactory dbContextFactory, IMapper mapper) 
    : BaseRepository<ReferralTest, ReferralTestDto>(dbContextFactory, mapper), IReferralTestRepository {
    private readonly DbContextFactory _dbContextFactory = dbContextFactory;
    private readonly IMapper _mapper = mapper;

    public async Task<ReferralTestDto> SaveReferralTestAsync(SaveReferralTestRequest referralTestRequest){
        await using var context = _dbContextFactory.CreateDbContext();

        var referralTest = new ReferralTest {
            Name = referralTestRequest.ReferralTestName,
            Description = referralTestRequest.ReferralTestDescription,
        };

        await context.ReferralTests.AddAsync(referralTest);
        await context.SaveChangesAsync();

        return _mapper.Map<ReferralTestDto>(referralTest);
    }

    public async Task<ReferralTestDto?> GetReferralTestByIdAsync(int referralTestId){
        return await GetEntityByIdAsync(referralTestId);
    }

    public async Task<ReferralTestDetailsDto?> GetReferralTestDetailsByIdAsync(int referralTestId){
        await using var context = _dbContextFactory.CreateDbContext();
        
        return await context.ReferralTests
            .Where(r => r.Id == referralTestId)
            .ProjectTo<ReferralTestDetailsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ReferralTestDto>> GetAllReferralTestsDtoAsync(){
        return await GetAllEntitiesAsync();
    }
}