using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Hospital.Domain.Entities;
public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Hospital.Domain.Entities.Hospital> Hospitals { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Define relationships and configure other entities

        //modelBuilder.Entity<Hospital.Domain.Entities.Hospital>()
        //    .HasMany(h => h.Departments)
        //    .WithOne(d => d.Hospital)
        //    .HasForeignKey(d => d.HospitalId)
        //    .OnDelete(DeleteBehavior.Cascade);

        //modelBuilder.Entity<Doctor>()
        //    .HasOne(d => d.User)
        //    .WithOne()
        //    .HasForeignKey<Doctor>(d => d.UserId);

        //modelBuilder.Entity<Patient>()
        //    .HasOne(p => p.User)
        //    .WithOne()
        //    .HasForeignKey<Patient>(p => p.UserId);
    }
}