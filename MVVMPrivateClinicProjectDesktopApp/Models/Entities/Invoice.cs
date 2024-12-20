using System;
using System.Collections.Generic;

namespace MVVMPrivateClinicProjectDesktopApp.Models.Entities;

public partial class Invoice
{
    public int Id { get; set; }

    public decimal Amount { get; set; }

    public DateTime DateIssued { get; set; }

    public DateTime? DueDate { get; set; }

    public string Status { get; set; } = null!;

    public int IdPatient { get; set; }

    public int IdPricing { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Patient IdPatientNavigation { get; set; } = null!;

    public virtual Pricing IdPricingNavigation { get; set; } = null!;
}
