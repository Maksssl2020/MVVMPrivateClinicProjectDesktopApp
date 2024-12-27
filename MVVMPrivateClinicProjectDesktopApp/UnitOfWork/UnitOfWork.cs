using AutoMapper;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Repositories.AddressRepository;
using MVVMPrivateClinicProjectDesktopApp.Repositories.AppointmentDateRepository;
using MVVMPrivateClinicProjectDesktopApp.Repositories.AppointmentRepository;
using MVVMPrivateClinicProjectDesktopApp.Repositories.DiagnosesRepository;
using MVVMPrivateClinicProjectDesktopApp.Repositories.DiseaseRepository;
using MVVMPrivateClinicProjectDesktopApp.Repositories.DoctorRepository;
using MVVMPrivateClinicProjectDesktopApp.Repositories.DoctorSpecializationRepository;
using MVVMPrivateClinicProjectDesktopApp.Repositories.InvoiceRepository;
using MVVMPrivateClinicProjectDesktopApp.Repositories.MedicineRepository;
using MVVMPrivateClinicProjectDesktopApp.Repositories.PatientNoteRepository;
using MVVMPrivateClinicProjectDesktopApp.Repositories.PatientRepository;
using MVVMPrivateClinicProjectDesktopApp.Repositories.PrescriptionRepository;
using MVVMPrivateClinicProjectDesktopApp.Repositories.PricingRepository;
using MVVMPrivateClinicProjectDesktopApp.Repositories.ReferralRepository;
using MVVMPrivateClinicProjectDesktopApp.Repositories.ReferralTestRepository;

namespace MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

public class UnitOfWork(DbContextFactory dbContextFactory) : IUnitOfWork {
    private readonly IMapper _mapper = MyMapper.Mapper;

    public DbContextFactory DbContextFactory { get; set; } = dbContextFactory;
    
    public IAddressRepository AddressRepository => new AddressRepository(DbContextFactory);
    public IAppointmentRepository AppointmentRepository => new AppointmentRepository(DbContextFactory, _mapper, PatientRepository, DoctorRepository, PricingRepository, InvoiceRepository);
    public IDiseaseRepository DiseaseRepository => new DiseaseRepository(DbContextFactory, _mapper);
    public IDoctorRepository DoctorRepository =>
        new DoctorRepository(DbContextFactory, _mapper, DoctorSpecializationRepository);
    public IDoctorSpecializationRepository DoctorSpecializationRepository => new DoctorSpecializationRepository(DbContextFactory, _mapper);
    public IMedicineRepository MedicineRepository => new MedicineRepository(DbContextFactory, _mapper);
    public IPatientRepository PatientRepository => new PatientRepository(DbContextFactory, _mapper);
    public IPrescriptionRepository PrescriptionRepository =>
        new PrescriptionRepository(DbContextFactory, _mapper, PatientRepository, DoctorRepository);
    public IReferralRepository ReferralRepository => new ReferralRepository(DbContextFactory, _mapper,
        PatientRepository, DoctorRepository, DiseaseRepository, ReferralTestRepository);
    public IInvoiceRepository InvoiceRepository => new InvoiceRepository(DbContextFactory, _mapper, PatientRepository, PricingRepository);
    public IPricingRepository PricingRepository => new PricingRepository(DbContextFactory, _mapper);
    public IPatientNoteRepository PatientNoteRepository => new PatientNoteRepository(DbContextFactory, _mapper, PatientRepository, DoctorRepository);
    public IReferralTestRepository ReferralTestRepository => new ReferralTestRepository(DbContextFactory, _mapper);
    public IDiagnosesRepository DiagnosesRepository => new DiagnosesRepository(DbContextFactory, _mapper, PatientRepository, DoctorRepository, DiseaseRepository);

    public IAppointmentDateRepository AppointmentDateRepository =>
        new AppointmentDateRepository(DbContextFactory, _mapper);

    public async Task SaveChangesAsync(){
        await using var context = DbContextFactory.CreateDbContext();
        await context.SaveChangesAsync();
    }
}