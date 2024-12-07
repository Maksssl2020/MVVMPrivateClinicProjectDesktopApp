using System;
using System.Collections.Generic;

namespace MVVMPrivateClinicProjectDesktopApp.Models.Entities;

public partial class Patient
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? EmailAddress { get; set; }

    public int IdAddress { get; set; }

    public int? IdPatientCard { get; set; }

    public string? PatientCode { get; set; }

    public virtual ICollection<AppointmentCard> AppointmentCards { get; set; } = new List<AppointmentCard>();

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();

    public virtual ICollection<DoctorClinicCard> DoctorClinicCards { get; set; } = new List<DoctorClinicCard>();

    public virtual Address IdAddressNavigation { get; set; } = null!;

    public virtual PatientClinicCard? IdPatientCardNavigation { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<PatientClinicCard> PatientClinicCards { get; set; } = new List<PatientClinicCard>();

    public virtual ICollection<PatientNote> PatientNotes { get; set; } = new List<PatientNote>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual ICollection<Referral> Referrals { get; set; } = new List<Referral>();
}
