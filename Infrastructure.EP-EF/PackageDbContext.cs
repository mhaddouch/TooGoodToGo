using Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EP_EF
{
    public class PackageDbContext : DbContext
    {

        public PackageDbContext(DbContextOptions<PackageDbContext> options) : base(options)
        {

        }
        public DbSet<Canteen> Canteens { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Product> Products { get; set; }
      public DbSet<Employee> Employees { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .Property(e => e.BirthDate)
                .HasColumnType("Date")
            .HasConversion(
            v => v.ToDateTime(TimeOnly.MinValue), // Convert DateOnly to DateTime
            v => DateOnly.FromDateTime(v)   // Convert DateTime to DateOnly
        );
        }
    }
}
