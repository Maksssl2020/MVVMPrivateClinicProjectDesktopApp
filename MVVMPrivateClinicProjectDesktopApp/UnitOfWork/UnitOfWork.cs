using AutoMapper;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Address;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Appointment;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Diagnosis;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Disease;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Doctor;
using MVVMPrivateClinicProjectDesktopApp.Repositories.DoctorSpecialization;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Invoice;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Medicine;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Note;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Prescription;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Pricing;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Referral;
using MVVMPrivateClinicProjectDesktopApp.Repositories.ReferralTest;

namespace MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

public class UnitOfWork(DbContextFactory dbContextFactory) : IUnitOfWork {
    private readonly IMapper _mapper = MyMapper.Mapper;
    
    public IAddressRepository AddressRepository => new AddressRepository(dbContextFactory);
    public IAppointmentRepository AppointmentRepository => new AppointmentRepository(dbContextFactory, PatientRepository, DoctorRepository);
    public IDiseaseRepository DiseaseRepository => new DiseaseRepository(dbContextFactory, _mapper);
    public IDoctorRepository DoctorRepository =>
        new DoctorRepository(dbContextFactory, _mapper, DoctorSpecializationRepository);
    public IDoctorSpecializationRepository DoctorSpecializationRepository => new DoctorSpecializationRepository(dbContextFactory, _mapper);
    public IMedicineRepository MedicineRepository => new MedicineRepository(dbContextFactory, _mapper);
    public IPatientRepository PatientRepository => new PatientRepository(dbContextFactory, _mapper);
    public IPrescriptionRepository PrescriptionRepository =>
        new PrescriptionRepository(dbContextFactory, _mapper, PatientRepository, DoctorRepository);
    public IReferralRepository ReferralRepository => new ReferralRepository(dbContextFactory, _mapper,
        PatientRepository, DoctorRepository, DiseaseRepository);
    public IInvoiceRepository InvoiceRepository => new InvoiceRepository(dbContextFactory, _mapper, PatientRepository);
    public IPricingRepository PricingRepository => new PricingRepository(dbContextFactory, _mapper);
    public IPatientNoteRepository PatientNoteRepository => new PatientNoteRepository(dbContextFactory, _mapper, PatientRepository, DoctorRepository);
    public IReferralTestRepository ReferralTestRepository => new ReferralTestRepository(dbContextFactory, _mapper);
    public IDiagnosisRepository DiagnosisRepository => new DiagnosisRepository(dbContextFactory, _mapper, PatientRepository, DoctorRepository, DiseaseRepository);

    public async Task SaveChangesAsync(){
        await using var context = dbContextFactory.CreateDbContext();
        await context.SaveChangesAsync();
    }
}