using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.ViewModels;

public class AppointmentsViewModel : ViewModelBase {
    private static readonly ObservableCollection<AppointmentDto> AppointmentDtos = [
        new() {
            Id = 1, DoctorFirstName = "Jane", DoctorLastName = "Smith", DoctorSpecialization = "Radiolog",
            PatientFirstName = "John", PatientLastName = "Doe",
            PatientCode = "PATJD1", AppointmentStatus = AppointmentStatus.Scheduled.ToString(),
            AppointmentDate = DateTime.Now.AddDays(new Random().Next(1, 10))
        },
        new() {
            Id = 2, DoctorFirstName = "Jane", DoctorLastName = "Smith", DoctorSpecialization = "Radiolog",
            PatientFirstName = "John", PatientLastName = "Doe",
            PatientCode = "PATJD1", AppointmentStatus = AppointmentStatus.Scheduled.ToString(),
            AppointmentDate = DateTime.Now.AddDays(new Random().Next(1, 10))
        },
        new() {
            Id = 3, DoctorFirstName = "Jane", DoctorLastName = "Smith", DoctorSpecialization = "Radiolog",
            PatientFirstName = "John", PatientLastName = "Doe",
            PatientCode = "PATJD1", AppointmentStatus = AppointmentStatus.Scheduled.ToString(),
            AppointmentDate = DateTime.Now.AddDays(new Random().Next(1, 10))
        },
        new() {
            Id = 4, DoctorFirstName = "Jane", DoctorLastName = "Smith", DoctorSpecialization = "Radiolog",
            PatientFirstName = "John", PatientLastName = "Doe",
            PatientCode = "PATJD1", AppointmentStatus = AppointmentStatus.Scheduled.ToString(),
            AppointmentDate = DateTime.Now.AddDays(new Random().Next(1, 10))
        },
        new() {
            Id = 5, DoctorFirstName = "Jane", DoctorLastName = "Smith", DoctorSpecialization = "Radiolog",
            PatientFirstName = "John", PatientLastName = "Doe",
            PatientCode = "PATJD1", AppointmentStatus = AppointmentStatus.Scheduled.ToString(),
            AppointmentDate = DateTime.Now.AddDays(new Random().Next(1, 10))
        },
        new() {
            Id = 6, DoctorFirstName = "Jane", DoctorLastName = "Smith", DoctorSpecialization = "Radiolog",
            PatientFirstName = "John", PatientLastName = "Doe",
            PatientCode = "PATJD1", AppointmentStatus = AppointmentStatus.Scheduled.ToString(),
            AppointmentDate = DateTime.Now.AddDays(new Random().Next(1, 10))
        },
        new() {
            Id = 7, DoctorFirstName = "Jane", DoctorLastName = "Smith", DoctorSpecialization = "Radiolog",
            PatientFirstName = "John", PatientLastName = "Doe",
            PatientCode = "PATJD1", AppointmentStatus = AppointmentStatus.Scheduled.ToString(),
            AppointmentDate = DateTime.Now.AddDays(new Random().Next(1, 10))
        },
        new() {
            Id = 8, DoctorFirstName = "Jane", DoctorLastName = "Smith", DoctorSpecialization = "Radiolog",
            PatientFirstName = "John", PatientLastName = "Doe",
            PatientCode = "PATJD1", AppointmentStatus = AppointmentStatus.Scheduled.ToString(),
            AppointmentDate = DateTime.Now.AddDays(new Random().Next(1, 10))
        },
        new() {
            Id = 9, DoctorFirstName = "Jane", DoctorLastName = "Smith", DoctorSpecialization = "Radiolog",
            PatientFirstName = "John", PatientLastName = "Doe",
            PatientCode = "PATJD1", AppointmentStatus = AppointmentStatus.Scheduled.ToString(),
            AppointmentDate = DateTime.Now.AddDays(new Random().Next(1, 10))
        },
        new() {
            Id = 10, DoctorFirstName = "Jane", DoctorLastName = "Smith", DoctorSpecialization = "Radiolog",
            PatientFirstName = "John", PatientLastName = "Doe",
            PatientCode = "PATJD1", AppointmentStatus = AppointmentStatus.Scheduled.ToString(),
            AppointmentDate = DateTime.Now.AddDays(new Random().Next(1, 10))
        }
    ];

    public ICollectionView AppointmentsView { get; set; } = CollectionViewSource.GetDefaultView(AppointmentDtos);
}