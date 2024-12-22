using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.ReferralTest;

public class ReferralTestRepository(DbContextFactory dbContextFactory, IMapper mapper) : IReferralTestRepository {
    public async Task<ReferralTestDto?> GetReferralTestByIdAsync(int referralTestId){
        await using var context = dbContextFactory.CreateDbContext();
        
        return await context.ReferralTests
            .Where(r => r.Id == referralTestId)
            .ProjectTo<ReferralTestDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<ReferralTestDetailsDto?> GetReferralTestDetailsByIdAsync(int referralTestId){
        await using var context = dbContextFactory.CreateDbContext();
        
        return await context.ReferralTests
            .Where(r => r.Id == referralTestId)
            .ProjectTo<ReferralTestDetailsDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ReferralTestDto>> GetAllReferralTestsDtoAsync(){
        await using var context = dbContextFactory.CreateDbContext();

        return await context.ReferralTests
            .ProjectTo<ReferralTestDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }
}