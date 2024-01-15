using Core.Domain;
using Infrastructure.EP_EF.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.EP_EF.SeedData;

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
        public DbSet<Voorbeeld> Voorbeelds { get;set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .Property(e => e.BirthDate)
                .HasColumnType("Date")
            .HasConversion(
            v => v.ToDateTime(TimeOnly.MinValue), // Convert DateOnly to DateTime
            v => DateOnly.FromDateTime(v));   // Convert DateTime to DateOnly

            
            modelBuilder.Entity<Employee>()
            .HasOne(e => e.Canteen)
            .WithMany() // Assuming each Canteen can have multiple employees
            .HasForeignKey(e => e.CanteenId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);



      

            /*modelBuilder.Entity<Package>()
          .HasOne(e => e.Canteen)
          .WithMany() // Assuming each Canteen can have multiple employees
          .HasForeignKey(e => e.CanteenId)
          .IsRequired(false)
          .OnDelete(DeleteBehavior.Cascade);*/

            
            
            //modelBuilder.Entity<Product>()
            //    .HasMany(e => e.Packages)
            //    .WithMany(e => e.Products);


           var seeder = new   EcoPlatesSeedData(modelBuilder);
           seeder.EnsurePopulated();

            




        }


    }
}
