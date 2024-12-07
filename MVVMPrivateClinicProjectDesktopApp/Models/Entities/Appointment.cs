using System;
using System.Collections.Generic;

namespace MVVMPrivateClinicProjectDesktopApp.Models.Entities;

public partial class Appointment
{
    public int Id { get; set; }

    public DateTime AppointmentDate { get; set; }

    public decimal? AppointmentCost { get; set; }

    public int IdPatient { get; set; }

    public int IdDoctor { get; set; }

    public string AppointmentStatus { get; set; } = null!;

    public int IdPricing { get; set; }

    public virtual ICollection<DoctorClinicCard> DoctorClinicCards { get; set; } = new List<DoctorClinicCard>();

    public virtual Doctor IdDoctorNavigation { get; set; } = null!;

    public virtual Patient IdPatientNavigation { get; set; } = null!;

    public virtual Pricing IdPricingNavigation { get; set; } = null!;

    public virtual ICollection<PatientClinicCard> PatientClinicCards { get; set; } = new List<PatientClinicCard>();
}
