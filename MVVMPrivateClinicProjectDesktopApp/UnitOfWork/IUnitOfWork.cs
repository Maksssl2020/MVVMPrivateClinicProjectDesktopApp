using MVVMPrivateClinicProjectDesktopApp.Repositories.Address;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Appointment;
using MVVMPrivateClinicProjectDesktopApp.Repositories.AppointmentDate;
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
    IDiagnosisRepository DiagnosisRepository { get; }
    IAppointmentDateRepository AppointmentDateRepository { get; }
    Task SaveChangesAsync();
}