using CarRentalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Data
{
    public class RentedCarDbContext : DbContext
    {
        public DbSet<RentedCar> RentedCars { get; set; }

        public RentedCarDbContext(DbContextOptions<RentedCarDbContext> options) : base(options)
        {
        }
    }
}