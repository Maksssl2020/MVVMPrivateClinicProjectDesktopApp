﻿using System;
using System.Collections.Generic;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;

namespace MVVMPrivateClinicProjectDesktopApp.Models.Entities;

public partial class Diagnosis : IDeletableEntity
{
    public int Id { get; set; }

    public DateOnly DiagnosisDate { get; set; }

    public string Description { get; set; } = null!;

    public int IdPatient { get; set; }

    public int IdDoctor { get; set; }

    public int IdDisease { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? DisabledDate { get; set; }

    public virtual Disease IdDiseaseNavigation { get; set; } = null!;

    public virtual Doctor IdDoctorNavigation { get; set; } = null!;

    public virtual Patient IdPatientNavigation { get; set; } = null!;

    public virtual ICollection<PatientClinicCard> PatientClinicCards { get; set; } = new List<PatientClinicCard>();
}
