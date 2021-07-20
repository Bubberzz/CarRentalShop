using CarRentalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Data
{
    public class CarDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }

        public CarDbContext(DbContextOptions<CarDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().HasData(
                new Car()
                {
                    Id = 1,
                    Name = "Audi R8",
                    Make = "Audi",
                    Model = "R8",
                    Year = 2020,
                    Status = "Available",
                    Price = 3100,
                    Stock = 1,
                });

            modelBuilder.Entity<Car>().HasData(
                new Car()
                {
                    Id = 2,
                    Name = "Land Rover Range Rover",
                    Make = "Land Rover",
                    Model = "Range Rover",
                    Year = 2016,
                    Status = "Available",
                    Price = 1000,
                    Stock = 9,
                });

            modelBuilder.Entity<Car>().HasData(
                new Car()
                {
                    Id = 3,
                    Name = "BMW X6 M Competition",
                    Make = "BMW",
                    Model = "X6 M Competition",
                    Year = 2010,
                    Status = "Available",
                    Price = 1080,
                    Stock = 4,
                });

            modelBuilder.Entity<Car>().HasData(
                new Car()
                {
                    Id = 4,
                    Name = "BMW X5 M",
                    Make = "BMW",
                    Model = "X5 M",
                    Year = 2009,
                    Status = "Available",
                    Price = 750,
                    Stock = 3,
                });

            modelBuilder.Entity<Car>().HasData(
                new Car()
                {
                    Id = 5,
                    Name = "Dodge RAM",
                    Make = "Dodge",
                    Model = "RAM",
                    Year = 2015,
                    Status = "Available",
                    Price = 1110,
                    Stock = 5,
                });

            modelBuilder.Entity<Car>().HasData(
                new Car()
                {
                    Id = 6,
                    Name = "Audi RSQ8",
                    Make = "Audi",
                    Model = "RSQ8",
                    Year = 2019,
                    Status = "Available",
                    Price = 1650,
                    Stock = 1,
                });

            modelBuilder.Entity<Car>().HasData(
                new Car()
                {
                    Id = 7,
                    Name = "Audi RS 6 Avant",
                    Make = "Audi",
                    Model = "RS 6 Avant",
                    Year = 2019,
                    Status = "Available",
                    Price = 1720,
                    Stock = 7,
                });

            modelBuilder.Entity<Car>().HasData(
                new Car()
                {
                    Id = 8,
                    Name = "Mercedes-Benz Gle Class",
                    Make = "Mercedes-Benz",
                    Model = "Gle Class",
                    Year = 2020,
                    Status = "Available",
                    Price = 1850,
                    Stock = 6,
                });

            modelBuilder.Entity<Car>().HasData(
                new Car()
                {
                    Id = 9,
                    Name = "BMW M5 Competition",
                    Make = "BMW",
                    Model = "M5 Competition",
                    Year = 2012,
                    Status = "Available",
                    Price = 990,
                    Stock = 8,
                });

            modelBuilder.Entity<Car>().HasData(
                new Car()
                {
                    Id = 10,
                    Name = "BMW M5",
                    Make = "BMW",
                    Model = "M5",
                    Year = 2018,
                    Status = "Available",
                    Price = 1276,
                    Stock = 2,
                });

            modelBuilder.Entity<Car>().HasData(
                new Car()
                {
                    Id = 11,
                    Name = "Jaguar F-Type",
                    Make = "Jaguar",
                    Model = "F-Type",
                    Year = 2021,
                    Status = "Available",
                    Price = 2750,
                    Stock = 1,
                });

            modelBuilder.Entity<Car>().HasData(
                new Car()
                {
                    Id = 12,
                    Name = "Jaguar I-Pace",
                    Make = "Jaguar",
                    Model = "I-Pace",
                    Year = 2021,
                    Status = "Available",
                    Price = 2600,
                    Stock = 2,
                });

            modelBuilder.Entity<Car>().HasData(
                new Car()
                {
                    Id = 13,
                    Name = "BMW 8 Series",
                    Make = "BMW",
                    Model = "8 Series",
                    Year = 2020,
                    Status = "Available",
                    Price = 1945,
                    Stock = 5,
                });

            modelBuilder.Entity<Car>().HasData(
                new Car()
                {
                    Id = 14,
                    Name = "Maserati Ghibli",
                    Make = "Maserati",
                    Model = "Ghibli",
                    Year = 2021,
                    Status = "Available",
                    Price = 2500,
                    Stock = 1,
                });

            modelBuilder.Entity<Car>().HasData(
                new Car()
                {
                    Id = 15,
                    Name = "Volvo XC90",
                    Make = "Volvo",
                    Model = "XC90",
                    Year = 2014,
                    Status = "Available",
                    Price = 700,
                    Stock = 15,
                });


        }
    }
}