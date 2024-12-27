using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;
using MVVMPrivateClinicProjectDesktopApp.Models;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using static System.Enum;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.DisabledDataRepository;

public class DisabledDataRepository<T>(
    DbContextFactory dbContextFactory
    ) : IDisabledDataRepository
    where T : class, IDeletableEntity {
    
    public async Task<IEnumerable<DisabledDataDto>> GetDisabledDataAsync(){
        await using var context = dbContextFactory.CreateDbContext();
        var foundDisabledEntities = await context.Set<T>()
            .Where(e => e.IsDeleted == true)
            .ToListAsync();


        var disabledDataDtos = CreateDisabledDataDto(foundDisabledEntities);

        return disabledDataDtos;
    }

    public async Task RestoreDataAsync(int dataId){
        await using var context = dbContextFactory.CreateDbContext();
        var foundData = await context.Set<T>()
            .FirstOrDefaultAsync(e => e.Id == dataId);

        if (foundData != null) {
            foundData.IsDeleted = false;
            await context.SaveChangesAsync();
        }
    }

    private IEnumerable<DisabledDataDto> CreateDisabledDataDto(IEnumerable<T> entities){
        List<DisabledDataDto> result = [];
        result.AddRange(entities.Select(entity => new DisabledDataDto { 
            Id = entity.Id,
            DisabledDataType = GetDisabledDataType(entity), 
            DisabledDate = entity.DisabledDate ?? DateTime.Now, 
            DataDetails = GetDisabledDataDetails(entity) 
        }));

        return result;
    }

    private static DisabledDataType GetDisabledDataType(T entity) => entity switch {
        Patient => DisabledDataType.Patient,
        Doctor => DisabledDataType.Doctor,
        PatientNote => DisabledDataType.PatientNote,
        DoctorSpecialization => DisabledDataType.DoctorSpecialization,
        Invoice => DisabledDataType.Invoice,
        Prescription => DisabledDataType.Prescription,
        Models.Entities.Referral  => DisabledDataType.Referral,
        Models.Entities.ReferralTest  => DisabledDataType.ReferralTest,
        Medicine  => DisabledDataType.Medicine,
        Diagnosis => DisabledDataType.Diagnosis,
        Models.Entities.Pricing  => DisabledDataType.Pricing,
        Disease => DisabledDataType.Disease,
        _ => DisabledDataType.Unknown
    };

    private static string GetDisabledDataDetails(T entity) => entity switch {
        Patient patient => $"{patient.FirstName} {patient.LastName} - {patient.PatientCode}",
        Doctor doctor => $"{doctor.FirstName} {doctor.LastName} - {doctor.DoctorCode}",
        PatientNote patientNote => $"{patientNote.Description}",
        DoctorSpecialization doctorSpecialization=> $"{doctorSpecialization.Name}",
        Invoice invoice => $"{invoice.Amount:C} Pricing ID:{invoice.IdPricing}",
        Prescription prescription => $"{prescription.PrescriptionDescription}",
        Models.Entities.Referral referral  => $"{referral.Description}",
        Models.Entities.ReferralTest  referralTest=> $"{referralTest.Name}",
        Medicine medicine => $"{medicine.Name}",
        Diagnosis diagnosis => $"{diagnosis.Description}",
        Models.Entities.Pricing pricing  => $"${pricing.ServiceName} {pricing.Price:C}",
        Disease disease => $"{disease.Name}",
        _ => "Unknown"
    };
}