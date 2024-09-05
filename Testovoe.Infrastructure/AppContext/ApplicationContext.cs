using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Testovoe.Models;

namespace Testovoe.Infrastructure.AppContext
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; } = null!;
        public DbSet<DoctorsRoom> DoctorsRooms { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<Region> Regions { get; set; } = null!;
        public DbSet<Specialization> Specializations { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            Database.EnsureCreated();   
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DoctorConfiguration());
        }
    }
}
