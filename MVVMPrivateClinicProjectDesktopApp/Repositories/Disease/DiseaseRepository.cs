using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Disease;

public class DiseaseRepository(DbContextFactory dbContextFactory, IMapper mapper) : IDiseaseRepository {
    private const string DiseaseCodeBeginning = "DIS";

    public async Task<DiseaseDto> SaveDiseaseAsync(SaveDiseaseRequest diseaseRequest){
        await using var context = dbContextFactory.CreateDbContext();
        
        var disease = new Models.Entities.Disease {
            Name = diseaseRequest.DiseaseName,
            DiseaseCode = await CreateDiseaseCode(diseaseRequest.DiseaseName, context)
        };

        await context.Diseases.AddAsync(disease);
        await context.SaveChangesAsync();

        return mapper.Map<DiseaseDto>(disease);
    }

    private static async Task<string> CreateDiseaseCode(string diseaseName, PrivateClinicContext context){
        var stringBuilder = new StringBuilder(DiseaseCodeBeginning);
        var splitDiseaseName = diseaseName.ToUpper().Split(' ');

        Console.WriteLine(splitDiseaseName);
        
        if (diseaseName.Length > 1) {
            foreach (var splitElement in splitDiseaseName) {
                stringBuilder.Append(splitElement.Trim()[..1]);
                Console.WriteLine(splitElement);
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
    
    public async Task<IEnumerable<DiseaseDto>> GetAllDiseasesAsync(){
        await using var context = dbContextFactory.CreateDbContext();
        return await context.Diseases
            .ProjectTo<DiseaseDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<DiseaseDto?> GetDiseaseByIdAsync(int diseaseId){
        await using var context = dbContextFactory.CreateDbContext();

        var foundDisease = context.Diseases
            .ProjectTo<DiseaseDto>(mapper.ConfigurationProvider)
            .FirstOrDefault(disease => disease.Id == diseaseId);

        return foundDisease;
    }
}