using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MVVMPrivateClinicProjectDesktopApp.Models.Entities;

public partial class PrivateClinicContext : DbContext
{
    public PrivateClinicContext()
    { }

    public PrivateClinicContext(DbContextOptions<PrivateClinicContext> options)
        : base(options)
    { }

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

    public virtual DbSet<Referral> Referrals { get; set; }

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
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointment_Doctor");

            entity.HasOne(d => d.IdPatientNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointment_Patient");
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

            entity.HasIndex(e => e.DoctorCode, "UK_DoctorCode_Table1").IsUnique();

            entity.Property(e => e.DoctorCode).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);

            entity.HasOne(d => d.IdDoctorCardNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.IdDoctorCard)
                .OnDelete(DeleteBehavior.ClientSetNull)
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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DoctorClinicCard_Appointment");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.DoctorClinicCards)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DoctorClinicCard_Doctor");

            entity.HasOne(d => d.IdPatientNavigation).WithMany(p => p.DoctorClinicCards)
                .HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull)
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
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.IdPatientNavigation).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Patient");
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

            entity.HasIndex(e => e.PatientCode, "UK_PatientCode_Table1").IsUnique();

            entity.Property(e => e.EmailAddress).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.PatientCode)
                .HasMaxLength(50)
                .ValueGeneratedOnAddOrUpdate()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            
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

            entity.Property(e => e.Description).HasMaxLength(1024);

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

            entity.HasOne(d => d.IdMedicineNavigation).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.IdMedicine)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescription_Medicine");

            entity.HasOne(d => d.IdPatientNavigation).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescription_Patient");
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
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
