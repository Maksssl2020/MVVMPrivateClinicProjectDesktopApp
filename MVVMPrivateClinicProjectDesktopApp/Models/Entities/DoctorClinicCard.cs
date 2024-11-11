using System;
using System.Collections.Generic;

namespace MVVMPrivateClinicProjectDesktopApp.Models;

public partial class DoctorClinicCard
{
    public int Id { get; set; }

    public int IdDoctor { get; set; }

    public int IdPatient { get; set; }

    public int IdAppointment { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual Appointment IdAppointmentNavigation { get; set; } = null!;

    public virtual Doctor IdDoctorNavigation { get; set; } = null!;

    public virtual Patient IdPatientNavigation { get; set; } = null!;
}
