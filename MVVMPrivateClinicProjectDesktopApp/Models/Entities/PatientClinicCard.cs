using System;
using System.Collections.Generic;

namespace MVVMPrivateClinicProjectDesktopApp.Models.Entities;

public partial class PatientClinicCard
{
    public int Id { get; set; }

    public int IdAppointment { get; set; }

    public int IdPatient { get; set; }

    public int IdPatientNote { get; set; }

    public int? IdPatientDiagnosis { get; set; }

    public int? IdMedicine { get; set; }

    public int? IdReferral { get; set; }

    public virtual Appointment IdAppointmentNavigation { get; set; } = null!;

    public virtual Medicine? IdMedicineNavigation { get; set; }

    public virtual Diagnosis? IdPatientDiagnosisNavigation { get; set; }

    public virtual Patient IdPatientNavigation { get; set; } = null!;

    public virtual PatientNote IdPatientNoteNavigation { get; set; } = null!;

    public virtual Referral? IdReferralNavigation { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
