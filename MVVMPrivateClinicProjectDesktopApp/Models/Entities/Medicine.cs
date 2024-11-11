using System;
using System.Collections.Generic;

namespace MVVMPrivateClinicProjectDesktopApp.Models;

public partial class Medicine
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<AppointmentCard> AppointmentCards { get; set; } = new List<AppointmentCard>();

    public virtual ICollection<PatientClinicCard> PatientClinicCards { get; set; } = new List<PatientClinicCard>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
