using System;
using System.Collections.Generic;

namespace MVVMPrivateClinicProjectDesktopApp.Models.Entities;

public partial class AppointmentDate
{
    public int Id { get; set; }

    public DateTime AppointmentDate1 { get; set; }

    public int IdDoctor { get; set; }

    public int IdPatient { get; set; }

    public virtual Doctor IdDoctorNavigation { get; set; } = null!;

    public virtual Patient IdPatientNavigation { get; set; } = null!;
}
