using System;
using System.Collections.Generic;
using MVVMPrivateClinicProjectDesktopApp.Models.Entities;

namespace MVVMPrivateClinicProjectDesktopApp.Models;

public partial class Doctor
{
    public int Id { get; set; }

    public string DoctorCode { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public int IdDoctorSpecialization { get; set; }

    public int IdDoctorCard { get; set; }

    public virtual ICollection<AppointmentCard> AppointmentCards { get; set; } = new List<AppointmentCard>();

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();

    public virtual ICollection<DoctorClinicCard> DoctorClinicCards { get; set; } = new List<DoctorClinicCard>();

    public virtual DoctorClinicCard IdDoctorCardNavigation { get; set; } = null!;

    public virtual DoctorSpecialization IdDoctorSpecializationNavigation { get; set; } = null!;

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual ICollection<Referral> Referrals { get; set; } = new List<Referral>();
}
