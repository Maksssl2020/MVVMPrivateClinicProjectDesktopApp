using MVVMPrivateClinicProjectDesktopApp.DbContext;
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

public interface IUnitOfWork {
    IAddressRepository AddressRepository { get; }
    IAppointmentRepository AppointmentRepository { get; }
    IDiseaseRepository DiseaseRepository { get; }
    IDoctorRepository DoctorRepository { get; }
    IDoctorSpecializationRepository DoctorSpecializationRepository { get; }
    IMedicineRepository MedicineRepository { get; }
    IPatientRepository PatientRepository { get; }
    IPrescriptionRepository PrescriptionRepository { get; }
    IReferralRepository ReferralRepository { get; }
    IInvoiceRepository InvoiceRepository { get; }
    IPricingRepository PricingRepository { get; }
    IPatientNoteRepository PatientNoteRepository { get; }
    IReferralTestRepository ReferralTestRepository { get; }
    IDiagnosesRepository DiagnosesRepository { get; }
    IAppointmentDateRepository AppointmentDateRepository { get; }
    DbContextFactory DbContextFactory { get; }
    Task SaveChangesAsync();
}