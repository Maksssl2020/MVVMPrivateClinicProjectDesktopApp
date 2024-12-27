using System;
using System.Collections.Generic;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;

namespace MVVMPrivateClinicProjectDesktopApp.Models.Entities;

public partial class Disease : IDeletableEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string DiseaseCode { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public DateTime? DisabledDate { get; set; }

    public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();

    public virtual ICollection<Referral> Referrals { get; set; } = new List<Referral>();
}
