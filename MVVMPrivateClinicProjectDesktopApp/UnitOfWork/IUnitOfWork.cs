using MVVMPrivateClinicProjectDesktopApp.Repositories.Address;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Appointment;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Disease;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Doctor;
using MVVMPrivateClinicProjectDesktopApp.Repositories.DoctorSpecialization;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Medicine;
using MVVMPrivateClinicProjectDesktopApp.Repositories.Patient;

namespace MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

public interface IUnitOfWork {
    IAddressRepository AddressRepository { get; }
    IAppointmentRepository AppointmentRepository { get; }
    IDiseaseRepository DiseaseRepository { get; }
    IDoctorRepository DoctorRepository { get; }
    IDoctorSpecializationRepository DoctorSpecializationRepository { get; }
    IMedicineRepository MedicineRepository { get; }
    IPatientRepository PatientRepository { get; }
    Task SaveChangesAsync();
}