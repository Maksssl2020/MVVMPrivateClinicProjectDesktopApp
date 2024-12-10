using System;
using System.Collections.Generic;

namespace MVVMPrivateClinicProjectDesktopApp.Models.Entities;

public partial class ReferralTest
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Referral> Referrals { get; set; } = new List<Referral>();
}
