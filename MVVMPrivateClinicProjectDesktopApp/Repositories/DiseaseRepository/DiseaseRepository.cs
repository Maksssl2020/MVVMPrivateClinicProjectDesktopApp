using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Base;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.DiseaseRepository;

public class DiseaseRepository(DbContextFactory dbContextFactory, IMapper mapper) 
    : BaseRepository<Disease, DiseaseDto>(dbContextFactory, mapper), IDiseaseRepository {
    private readonly DbContextFactory _dbContextFactory = dbContextFactory;
    private readonly IMapper _mapper = mapper;
    private const string DiseaseCodeBeginning = "DIS";

    public async Task<DiseaseDto> SaveDiseaseAsync(SaveDiseaseRequest diseaseRequest){
        await using var context = _dbContextFactory.CreateDbContext();
        
        var disease = new Disease {
            Name = diseaseRequest.DiseaseName,
            DiseaseCode = await CreateDiseaseCode(diseaseRequest.DiseaseName, context)
        };

        await context.Diseases.AddAsync(disease);
        await context.SaveChangesAsync();

        return _mapper.Map<DiseaseDto>(disease);
    }

    private static async Task<string> CreateDiseaseCode(string diseaseName, PrivateClinicContext context){
        var stringBuilder = new StringBuilder(DiseaseCodeBeginning);
        var splitDiseaseName = diseaseName.ToUpper().Split(' ');

        if (diseaseName.Length > 1) {
            foreach (var splitElement in splitDiseaseName) {
                stringBuilder.Append(splitElement.Trim()[..1]);
            }
        }
        else {
            var firstThreeLetterOfDiseaseName = diseaseName[..3];
            stringBuilder.Append(firstThreeLetterOfDiseaseName);
        }

        var maxId = await context.Diseases.MaxAsync(d => d.Id) + 1;
        stringBuilder.Append(maxId);
        
        return stringBuilder.ToString();
    }

    public async Task<DiseaseDetailsDto?> GetDiseaseDetailsByIdAsync(int diseaseId){
        await using var context = _dbContextFactory.CreateDbContext();

        return await context.Diseases
            .Where(d => d.Id == diseaseId)
            .ProjectTo<DiseaseDetailsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<DiseaseDto>> GetAllDiseasesAsync(){
        return await GetAllEntitiesAsync();
    }

    public async Task<DiseaseDto?> GetDiseaseByIdAsync(int diseaseId){
        return await GetEntityByIdAsync(diseaseId);
    }
}