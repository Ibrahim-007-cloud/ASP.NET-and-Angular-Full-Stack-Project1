using Microsoft.EntityFrameworkCore;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Infrastructure.Data
{
    public class ClinicDbContext : DbContext
    {
        public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Visit> Visits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Explicitly Map the Patient -> Visits Relationship with Cascade Delete
            modelBuilder.Entity<Visit>()
                .HasOne(v => v.Patient)
                .WithMany(p => p.Visits)
                .HasForeignKey(v => v.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // 2. Explicitly Map the Doctor -> Visits Relationship 
            // SQLite prefers Restrict/NoAction for multiple relationships to avoid cascading conflicts
            modelBuilder.Entity<Visit>()
                .HasOne(v => v.Doctor)
                .WithMany(d => d.Visits)
                .HasForeignKey(v => v.DoctorId)
                .OnDelete(DeleteBehavior.Restrict); 

            // 3. Seed initial Doctor records exactly matching required criteria
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { Id = 1, Name = "Dr. Asim Ali", Specialization = "General Physician" },
                new Doctor { Id = 2, Name = "Dr. Sarah Khan", Specialization = "Pediatrician" },
                new Doctor { Id = 3, Name = "Dr. Hamza Ahmed", Specialization = "Cardiologist" }
            );
        }
    }
}