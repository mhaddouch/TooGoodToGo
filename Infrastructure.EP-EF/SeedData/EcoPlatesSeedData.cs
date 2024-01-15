using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EP_EF.SeedData
{
    public class EcoPlatesSeedData
    {
        private ModelBuilder _modelbuilder;
        //private ModelBuilder modelBuilder;

        public EcoPlatesSeedData(ModelBuilder modelBuilder)
        {
            _modelbuilder = modelBuilder;
        }

        public async Task EnsurePopulated(bool dropExisting = false)
        {


            // Remove related Employees
            //_context.Employees.RemoveRange(_context.Employees);

            // Remove existing Canteens
            // _context.Canteens.RemoveRange(_context.Canteens);



            Canteen Canteen1 = new Canteen { Id = 1, City = City.Breda, LocationName = "LA", OfferHotMeals = true };
            Canteen Canteen2 = new Canteen { Id = 2, City = City.Eindhoven, LocationName = "LD", OfferHotMeals = true };
            Canteen Canteen3 = new Canteen { Id = 3, City = City.Breda, LocationName = "HA", OfferHotMeals = true };
            Canteen Canteen4 = new Canteen { Id = 4, City = City.Breda, LocationName = "LA", OfferHotMeals = true };
            /*_context.Canteens.AddRange(new[]
             {
                 Canteen1,
                 Canteen2,
                 Canteen3,
             });*/
            // await _context.SaveChangesAsync();

            _modelbuilder.Entity<Canteen>().HasData(
                Canteen1,
                Canteen2,
                Canteen3,
                Canteen4
                );


            //products

            Product product1 = new Product { Id = 1, Name = "bread", ContainsAlcohol = false, PhotoPath = "https://i0.wp.com/www.vickyvandijk.nl/wp-content/uploads/2020/04/Vicky-van-Dijk-Knapperig-wit-brood-03.jpg?fit=1500%2C2100&ssl=1" };
            Product product2 = new Product { Id = 2, Name = "apple", ContainsAlcohol = false, PhotoPath = "https://images.nrc.nl/iV2oqfYUkk7SP_itBSSOkEk6-TE=/1280x/filters:no_upscale()/s3/static.nrc.nl/images/gn4/stripped/data93925993-1e8a11.jpg" };
            Product product3 = new Product { Id = 3, Name = "milk", ContainsAlcohol = false, PhotoPath = "https://images.unsplash.com/photo-1588710929895-6ee7a0a4d155?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MjR8fG1pbGt8ZW58MHx8MHx8fDA%3D" };
            _modelbuilder.Entity<Product>().HasData(
                product1,
                product2,
                product3
                );

        

            Package package1 = new Package 
            { 
                Id = 1, 
                Name = "Healthy meal", 
                RetrieveDate = new DateTime(2024, 3, 15), 
                DeadLineRetriveDate = new DateTime(2024, 4, 9), 
                ReserverdByStudent = null, 
                City = Canteen1.City, 
                Price = 8, 
                Meal = Meal.ontbijt,
                CanteenId = Canteen1.Id
            };
            Package package2 = new Package { Id = 2, Name = "Lots of bread", RetrieveDate = new DateTime(2024, 3, 15), DeadLineRetriveDate = new DateTime(2024, 4, 9), ReserverdByStudent = null, City = Canteen1.City, Price = 8, Meal = Meal.avondmaaltijd, CanteenId =Canteen1.Id };
            //package2.Products.Add(product1);
            //product2.Packages.Add( package1 );
            //package1.AddProduct(product2);
            //package1.AddProduct(product3);
            //package2.AddProduct(product1);
            //package2.AddProduct(product3);
            _modelbuilder.Entity<Package>().HasData(
                package1,
                package2
                );


        }

    }

}