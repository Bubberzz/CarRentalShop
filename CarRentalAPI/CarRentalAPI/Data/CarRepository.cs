using System;
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

        public async Task<string> RentCar(int id, int rentPeriod)
        {
            var car = _carsContext.Cars.FirstOrDefault(c => c.Id == id);
            var ts = new TimeSpan(0, rentPeriod, 0);

            if (car is not null && car.Stock is not 0)
            {
                var stock = car.Stock;
                stock -= 1;
                car.Stock = stock;

                // check if ID is already in db, if yes then increment
                while (_rentedCarsContext.RentedCars.Any(c => c.Id == id))
                {
                    id += 1;
                }

                var expiryDate = DateTime.Now + ts;
                var rentCar = new RentedCar()
                {
                    Id = id,
                    Name = car.Name,
                    Make = car.Make,
                    Model = car.Model,
                    Year = car.Year,
                    Status = "Rented",
                    Price = car.Price,
                    ExpiryDate = expiryDate
                };

                _rentedCarsContext.RentedCars.Add(rentCar);
            }
            else
            {
                return "Car not in stock";
            }
            SaveChanges();
            return "Car successfully rented";
        }
        
        // public Task<string> ReturnCar(int id)
        // {
        //
        // }
        //
        //
        // public Task CheckExpiry()
        // {
        //   
        // }
        
        // private Task SendNotification(string carName)
        // {
        //
        // }

        private void SaveChanges()
        {
            _rentedCarsContext.SaveChangesAsync();
            _carsContext.SaveChangesAsync();
        }
    }
}