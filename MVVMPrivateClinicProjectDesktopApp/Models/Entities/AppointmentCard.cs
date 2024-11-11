using System;
using System.Collections.Generic;

namespace MVVMPrivateClinicProjectDesktopApp.Models;

public partial class AppointmentCard
{
    public int Id { get; set; }

    public DateTime DateIssued { get; set; }

    public string Description { get; set; } = null!;

    public int IdPatient { get; set; }

    public int IdDoctor { get; set; }

    public int? IdMedicine { get; set; }

    public int? IdReferral { get; set; }

    public virtual Doctor IdDoctorNavigation { get; set; } = null!;

    public virtual Medicine? IdMedicineNavigation { get; set; }

    public virtual Patient IdPatientNavigation { get; set; } = null!;

    public virtual Referral? IdReferralNavigation { get; set; }
}
