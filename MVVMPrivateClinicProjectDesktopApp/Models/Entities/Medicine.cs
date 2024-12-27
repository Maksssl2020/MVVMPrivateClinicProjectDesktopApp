using System;
using System.Collections.Generic;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;

namespace MVVMPrivateClinicProjectDesktopApp.Models.Entities;

public partial class Medicine : IDeletableEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public DateTime? DisabledDate { get; set; }

    public virtual ICollection<AppointmentCard> AppointmentCards { get; set; } = new List<AppointmentCard>();

    public virtual ICollection<PatientClinicCard> PatientClinicCards { get; set; } = new List<PatientClinicCard>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
