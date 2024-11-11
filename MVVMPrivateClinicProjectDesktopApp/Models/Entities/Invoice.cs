using System;
using System.Collections.Generic;

namespace MVVMPrivateClinicProjectDesktopApp.Models;

public partial class Invoice
{
    public int Id { get; set; }

    public decimal Amount { get; set; }

    public DateTime DateIssued { get; set; }

    public DateOnly DueDate { get; set; }

    public string Status { get; set; } = null!;

    public int IdPatient { get; set; }

    public virtual Patient IdPatientNavigation { get; set; } = null!;
}
