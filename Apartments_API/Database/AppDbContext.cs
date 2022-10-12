using Apartment_API.Models;
using Apartments_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Apartment_API.Database
{
    
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }
        public DbSet<Apartment> Apartments { get; set; } //name of the table
        public DbSet<ApartmentNumber> ApartmentNumbers { get; set; } //name of the table

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apartment>().HasData(
                new Apartment
                {
                    Id = 1,
                    Name = "Luxury Apartment",
                    Details = "Luxury treehouse apartment with lake view.",
                    ImagePath = "https://dotnetmastery.com/bluevillaimages/villa3.jpg",
                    Occupancy = 4,
                    Rate = 200,
                    SquareFeet = 750,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                },
              new Apartment
              {
                  Id = 2,
                  Name = "Premium Pool Apartment",
                  Details = "Stylish apartment with pool amidst palm trees and with sea view.",
                  ImagePath = "https://dotnetmastery.com/bluevillaimages/villa1.jpg",
                  Occupancy = 4,
                  Rate = 300,
                  SquareFeet = 900,
                  Amenity = "",
                  CreatedDate = DateTime.Now,
              },
              new Apartment
              {
                  Id = 3,
                  Name = "Stylish Pool Apartment",
                  Details = "Luxury apartment with pool amidst palm trees and with sea view..",
                  ImagePath = "https://dotnetmastery.com/bluevillaimages/villa4.jpg",
                  Occupancy = 4,
                  Rate = 400,
                  SquareFeet = 600,
                  Amenity = "",
                  CreatedDate = DateTime.Now,
              },
              new Apartment
              {
                  Id = 4,
                  Name = "Diamond Apartment",
                  Details = "Luxurious apartment in Mediterranean style, built on a cliff overlooking the sea.",
                  ImagePath = "https://dotnetmastery.com/bluevillaimages/villa5.jpg",
                  Occupancy = 4,
                  Rate = 550,
                  SquareFeet = 950,
                  Amenity = "",
                  CreatedDate = DateTime.Now,
              },
              new Apartment
              {
                  Id = 5,
                  Name = "Diamond Pool Villa",
                  Details = "Luxury apartment with views of hollywood.",
                  ImagePath = "https://dotnetmastery.com/bluevillaimages/villa2.jpg",
                  Occupancy = 4,
                  Rate = 600,
                  SquareFeet = 900,
                  Amenity = "",
                  CreatedDate = DateTime.Now,
              });
        }
    }
}
