using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalAPI.Data;
using CarRentalAPI.Interfaces;
using CarRentalAPI.Models;
using Microsoft.Extensions.Configuration;


namespace CarRental.Data
{
    public class CarRepository : ICarRepository
    {
        private readonly CarDbContext _carsContext;
        private readonly RentedCarDbContext _rentedCarsContext;
        private readonly IConfiguration _config;

        public CarRepository(CarDbContext carsContext, RentedCarDbContext carsRentedContext, IConfiguration config)
        {
            _carsContext = carsContext;
            _rentedCarsContext = carsRentedContext;
            _config = config;
            _carsContext.Database.EnsureCreated();
        }

        public async Task<IEnumerable<Car>> GetCars()
        {
            return _carsContext.Cars.ToList();
        }

        public async Task<IEnumerable<RentedCar>> GetRentedCars()
        {
            return _rentedCarsContext.RentedCars.ToList();
        }

        public Task<string> RentCar(int id, int rentPeriod)
        {
        
        }
        
        public Task<string> ReturnCar(int id)
        {
        
        }
        
        
        public Task CheckExpiry()
        {
          
        }
        
        private Task SendNotification(string carName)
        {
        
        }

        private void SaveChanges()
        {

        }
    }
}