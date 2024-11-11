using System;
using System.Collections.Generic;

namespace MVVMPrivateClinicProjectDesktopApp.Models;

public partial class Disease
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string DiseaseCode { get; set; } = null!;

    public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();

    public virtual ICollection<Referral> Referrals { get; set; } = new List<Referral>();
}
