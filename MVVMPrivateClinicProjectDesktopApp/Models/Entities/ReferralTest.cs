using System;
using System.Collections.Generic;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;

namespace MVVMPrivateClinicProjectDesktopApp.Models.Entities;

public partial class ReferralTest : IDeletableEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public DateTime? DisabledDate { get; set; }

    public virtual ICollection<Referral> Referrals { get; set; } = new List<Referral>();
}
