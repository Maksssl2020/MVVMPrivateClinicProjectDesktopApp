using System;
using System.Collections.Generic;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;

namespace MVVMPrivateClinicProjectDesktopApp.Models.Entities;

public partial class Pricing : IDeletableEntity
{
    public int Id { get; set; }

    public string ServiceName { get; set; } = null!;

    public string ServiceType { get; set; } = null!;

    public decimal Price { get; set; }

    public bool? IsDeleted { get; set; }

    public DateOnly EffectiveDate { get; set; }

    public DateTime? DisabledDate { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
