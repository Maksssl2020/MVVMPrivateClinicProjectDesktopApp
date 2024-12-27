using System;
using System.Collections.Generic;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;

namespace MVVMPrivateClinicProjectDesktopApp.Models.Entities;

public partial class Prescription : IDeletableEntity
{
    public int Id { get; set; }

    public DateOnly DateIssued { get; set; }

    public DateOnly? ExpirationDate { get; set; }

    public string PrescriptionDescription { get; set; } = null!;

    public string PrescriptionCode { get; set; } = null!;

    public int IdPatient { get; set; }

    public int IdDoctor { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? DisabledDate { get; set; }

    public virtual Doctor IdDoctorNavigation { get; set; } = null!;

    public virtual Patient IdPatientNavigation { get; set; } = null!;

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}
