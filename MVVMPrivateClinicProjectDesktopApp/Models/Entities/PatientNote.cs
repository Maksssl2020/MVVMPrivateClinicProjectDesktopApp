﻿namespace MVVMPrivateClinicProjectDesktopApp.Models.Entities;

public partial class PatientNote
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public int IdPatient { get; set; }

    public virtual Patient IdPatientNavigation { get; set; } = null!;

    public virtual ICollection<PatientClinicCard> PatientClinicCards { get; set; } = new List<PatientClinicCard>();
}
