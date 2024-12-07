using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.ReferralTest;

public class ReferralTestRepository(DbContextFactory dbContextFactory, IMapper mapper) : IReferralTestRepository {
    public async Task<IEnumerable<ReferralTestDto>> GetAllReferralTestsDtoAsync(){
        await using var context = dbContextFactory.CreateDbContext();

        return await context.ReferralTests
            .ProjectTo<ReferralTestDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }
}