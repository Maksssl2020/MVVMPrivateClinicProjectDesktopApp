using AutoMapper;
using MVVMPrivateClinicProjectDesktopApp.DbContext;
using MVVMPrivateClinicProjectDesktopApp.Helpers;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Address;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Appointment;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Disease;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Doctor;
using MVVMPrivateClinicProjectDesktopApp.Repositories.DoctorSpecialization;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Medicine;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

namespace MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

public class UnitOfWork(DbContextFactory dbContextFactory) : IUnitOfWork {
    private readonly IMapper _mapper = MyMapper.Mapper;
    
    public IAddressRepository AddressRepository => new AddressRepository(dbContextFactory);
    public IAppointmentRepository AppointmentRepository => new AppointmentRepository(dbContextFactory, PatientRepository, DoctorRepository);
    public IDiseaseRepository DiseaseRepository => new DiseaseRepository(dbContextFactory);
    public IDoctorRepository DoctorRepository =>
        new DoctorRepository(dbContextFactory, _mapper, DoctorSpecializationRepository);
    public IDoctorSpecializationRepository DoctorSpecializationRepository => new DoctorSpecializationRepository(dbContextFactory);
    public IMedicineRepository MedicineRepository => new MedicineRepository(dbContextFactory, _mapper);
    public IPatientRepository PatientRepository => new PatientRepository(dbContextFactory);

    public async Task SaveChangesAsync(){
        await using var context = dbContextFactory.CreateDbContext();
        await context.SaveChangesAsync();
    }
}