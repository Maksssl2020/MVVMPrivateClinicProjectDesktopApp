using System;
using System.Collections.Generic;

namespace MVVMPrivateClinicProjectDesktopApp.Models;

public partial class Referral
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime DateIssued { get; set; }

    public int IdPatient { get; set; }

    public int IdDoctor { get; set; }

    public int? IdDisease { get; set; }

    public virtual ICollection<AppointmentCard> AppointmentCards { get; set; } = new List<AppointmentCard>();

    public virtual Disease? IdDiseaseNavigation { get; set; }

    public virtual Doctor IdDoctorNavigation { get; set; } = null!;

    public virtual Patient IdPatientNavigation { get; set; } = null!;

    public virtual ICollection<PatientClinicCard> PatientClinicCards { get; set; } = new List<PatientClinicCard>();
}
