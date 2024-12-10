using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVVMPrivateClinicProjectDesktopApp.Models.Entities;

public partial class PrivateClinicContext : Microsoft.EntityFrameworkCore.DbContext
{
    public PrivateClinicContext()
    {
    }

    public PrivateClinicContext(DbContextOptions<PrivateClinicContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<AppointmentCard> AppointmentCards { get; set; }

    public virtual DbSet<Diagnosis> Diagnoses { get; set; }

    public virtual DbSet<Disease> Diseases { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoctorClinicCard> DoctorClinicCards { get; set; }

    public virtual DbSet<DoctorSpecialization> DoctorSpecializations { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientClinicCard> PatientClinicCards { get; set; }

    public virtual DbSet<PatientNote> PatientNotes { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<Pricing> Pricings { get; set; }

    public virtual DbSet<Referral> Referrals { get; set; }

    public virtual DbSet<ReferralTest> ReferralTests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=PrivateClinic;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Address");

            entity.Property(e => e.BuildingNumber).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.LocalNumber).HasMaxLength(255);
            entity.Property(e => e.PostalCode).HasMaxLength(50);
            entity.Property(e => e.Street).HasMaxLength(255);
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.ToTable("Appointment");

            entity.Property(e => e.AppointmentCost).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.AppointmentDate).HasColumnType("datetime");
            entity.Property(e => e.AppointmentStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Scheduled");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointment_Doctor");

            entity.HasOne(d => d.IdPatientNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointment_Patient");

            entity.HasOne(d => d.IdPricingNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.IdPricing)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointment_Pricing");
        });

        modelBuilder.Entity<AppointmentCard>(entity =>
        {
            entity.ToTable("AppointmentCard");

            entity.Property(e => e.DateIssued).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.AppointmentCards)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppointmentCard_Doctor");

            entity.HasOne(d => d.IdMedicineNavigation).WithMany(p => p.AppointmentCards)
                .HasForeignKey(d => d.IdMedicine)
                .HasConstraintName("FK_AppointmentCard_Medicine");

            entity.HasOne(d => d.IdPatientNavigation).WithMany(p => p.AppointmentCards)
                .HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppointmentCard_Patient");

            entity.HasOne(d => d.IdReferralNavigation).WithMany(p => p.AppointmentCards)
                .HasForeignKey(d => d.IdReferral)
                .HasConstraintName("FK_AppointmentCard_Referral");
        });

        modelBuilder.Entity<Diagnosis>(entity =>
        {
            entity.ToTable("Diagnosis");

            entity.Property(e => e.Description).HasMaxLength(255);

            entity.HasOne(d => d.IdDiseaseNavigation).WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.IdDisease)
                .HasConstraintName("FK_Diagnosis_Disease");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Diagnosis_Doctor");

            entity.HasOne(d => d.IdPatientNavigation).WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Diagnosis_Patient");
        });

        modelBuilder.Entity<Disease>(entity =>
        {
            entity.ToTable("Disease");

            entity.HasIndex(e => e.DiseaseCode, "UK_DiseaseCode_Table1").IsUnique();

            entity.Property(e => e.DiseaseCode).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.ToTable("Doctor");

            entity.Property(e => e.DoctorCode)
                .HasMaxLength(15)
                .HasComputedColumnSql("((('DOC'+upper(left([FirstName],(1))))+upper(left([LastName],(1))))+CONVERT([varchar](10),[Id]))", false);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);

            entity.HasOne(d => d.IdDoctorCardNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.IdDoctorCard)
                .HasConstraintName("FK_Doctor_DoctorClinicCard");

            entity.HasOne(d => d.IdDoctorSpecializationNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.IdDoctorSpecialization)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Doctor_DoctorSpecialization");
        });

        modelBuilder.Entity<DoctorClinicCard>(entity =>
        {
            entity.ToTable("DoctorClinicCard");

            entity.HasOne(d => d.IdAppointmentNavigation).WithMany(p => p.DoctorClinicCards)
                .HasForeignKey(d => d.IdAppointment)
                .HasConstraintName("FK_DoctorClinicCard_Appointment");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.DoctorClinicCards)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DoctorClinicCard_Doctor");

            entity.HasOne(d => d.IdPatientNavigation).WithMany(p => p.DoctorClinicCards)
                .HasForeignKey(d => d.IdPatient)
                .HasConstraintName("FK_DoctorClinicCard_Patient");
        });

        modelBuilder.Entity<DoctorSpecialization>(entity =>
        {
            entity.ToTable("DoctorSpecialization");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.ToTable("Invoice");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.DateIssued).HasColumnType("datetime");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.IdPatientNavigation).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Patient");

            entity.HasOne(d => d.IdPricingNavigation).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.IdPricing)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Pricing");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.ToTable("Medicine");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(255);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("Patient");

            entity.HasIndex(e => e.EmailAddress, "UK_EmailAddress_Table1").IsUnique();

            entity.Property(e => e.EmailAddress).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.PatientCode)
                .HasMaxLength(35)
                .HasComputedColumnSql("(upper((('PAT'+left([FirstName],(1)))+left([LastName],(1)))+CONVERT([nvarchar],[Id])))", true);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);

            entity.HasOne(d => d.IdAddressNavigation).WithMany(p => p.Patients)
                .HasForeignKey(d => d.IdAddress)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patient_Address");

            entity.HasOne(d => d.IdPatientCardNavigation).WithMany(p => p.Patients)
                .HasForeignKey(d => d.IdPatientCard)
                .HasConstraintName("FK_Patient_PatientClinicCard");
        });

        modelBuilder.Entity<PatientClinicCard>(entity =>
        {
            entity.ToTable("PatientClinicCard");

            entity.HasOne(d => d.IdAppointmentNavigation).WithMany(p => p.PatientClinicCards)
                .HasForeignKey(d => d.IdAppointment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientClinicCard_Appointment");

            entity.HasOne(d => d.IdMedicineNavigation).WithMany(p => p.PatientClinicCards)
                .HasForeignKey(d => d.IdMedicine)
                .HasConstraintName("FK_PatientClinicCard_Medicine");

            entity.HasOne(d => d.IdPatientNavigation).WithMany(p => p.PatientClinicCards)
                .HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientClinicCard_Patient");

            entity.HasOne(d => d.IdPatientDiagnosisNavigation).WithMany(p => p.PatientClinicCards)
                .HasForeignKey(d => d.IdPatientDiagnosis)
                .HasConstraintName("FK_PatientClinicCard_Diagnosis");

            entity.HasOne(d => d.IdPatientNoteNavigation).WithMany(p => p.PatientClinicCards)
                .HasForeignKey(d => d.IdPatientNote)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientClinicCard_PatientNote");

            entity.HasOne(d => d.IdReferralNavigation).WithMany(p => p.PatientClinicCards)
                .HasForeignKey(d => d.IdReferral)
                .HasConstraintName("FK_PatientClinicCard_Referral");
        });

        modelBuilder.Entity<PatientNote>(entity =>
        {
            entity.ToTable("PatientNote");

            entity.Property(e => e.DateIsuued).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(1024);

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.PatientNotes)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientNote_Doctor");

            entity.HasOne(d => d.IdPatientNavigation).WithMany(p => p.PatientNotes)
                .HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientNote_Patient");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.ToTable("Prescription");

            entity.HasIndex(e => e.PrescriptionCode, "UK_PrescriptionCode_Table1").IsUnique();

            entity.Property(e => e.PrescriptionCode).HasMaxLength(50);
            entity.Property(e => e.PrescriptionDescription).HasMaxLength(255);

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescription_Doctor");

            entity.HasOne(d => d.IdPatientNavigation).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescription_Patient");

            entity.HasMany(d => d.Medicines).WithMany(p => p.Prescriptions)
                .UsingEntity<Dictionary<string, object>>(
                    "PrescriptionMedicine",
                    r => r.HasOne<Medicine>().WithMany()
                        .HasForeignKey("MedicineId")
                        .HasConstraintName("FK__Prescript__Medic__4959E263"),
                    l => l.HasOne<Prescription>().WithMany()
                        .HasForeignKey("PrescriptionId")
                        .HasConstraintName("FK__Prescript__Presc__4865BE2A"),
                    j =>
                    {
                        j.HasKey("PrescriptionId", "MedicineId").HasName("PK__Prescrip__54E11ABB3BD12D4E");
                        j.ToTable("PrescriptionMedicine");
                    });
        });

        modelBuilder.Entity<Pricing>(entity =>
        {
            entity.ToTable("Pricing");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ServiceName).HasMaxLength(255);
            entity.Property(e => e.ServiceType).HasMaxLength(255);
        });

        modelBuilder.Entity<Referral>(entity =>
        {
            entity.ToTable("Referral");

            entity.Property(e => e.DateIssued).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.IdDiseaseNavigation).WithMany(p => p.Referrals)
                .HasForeignKey(d => d.IdDisease)
                .HasConstraintName("FK_Referral_Disease");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Referrals)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Referral_Doctor");

            entity.HasOne(d => d.IdPatientNavigation).WithMany(p => p.Referrals)
                .HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Referral_Patient");

            entity.HasOne(d => d.IdReferralTestNavigation).WithMany(p => p.Referrals)
                .HasForeignKey(d => d.IdReferralTest)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Referral_ReferralTest");
        });

        modelBuilder.Entity<ReferralTest>(entity =>
        {
            entity.ToTable("ReferralTest");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
