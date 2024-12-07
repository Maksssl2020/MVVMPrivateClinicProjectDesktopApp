using System;
using System.Collections.Generic;

namespace MVVMPrivateClinicProjectDesktopApp.Models.Entities;

public partial class Pricing
{
    public int Id { get; set; }

    public string ServiceName { get; set; } = null!;

    public string ServiceType { get; set; } = null!;

    public decimal Price { get; set; }

    public bool IsAvailable { get; set; }

    public DateOnly EffectiveDate { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
