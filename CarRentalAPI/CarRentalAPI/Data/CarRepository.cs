using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CarRentalAPI.Data;
using CarRentalAPI.Interfaces;
using CarRentalAPI.Models;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;


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
        
        public async Task<string> ReturnCar(int id)
        {
            var car = _carsContext.Cars.FirstOrDefault(c => c.Id == id);
            var rentedCar = _rentedCarsContext.RentedCars.FirstOrDefault(c => c.Name == car.Name);

            if (car is not null)
            {
                var stock = car.Stock;
                stock += 1;
                car.Stock = stock;
            }

            if (rentedCar is not null)
            {
                _rentedCarsContext.RentedCars.Remove(rentedCar);
            }
            else
            {
                return "No cars to return";
            }

            SaveChanges();
            return "Car successfully returned";
        }
        
        

        public async Task CheckExpiry()
        {
            var ts = new TimeSpan(0, 01, 0);
            var rentedCars = _rentedCarsContext.RentedCars.ToList();
            
            foreach (var car in rentedCars)
            {
                var aboutToExpire = car.ExpiryDate - ts;
                await SendNotification(car.Name);

                if (car.ExpiryDate <= DateTime.Now)
                {
                    car.Status = "Expired";
                    SaveChanges();
                }
                else if (DateTime.Now >= aboutToExpire &&
                         DateTime.Now < car.ExpiryDate &&
                         car.NotificationSent is false)
                {
                    car.NotificationSent = true;
                    await SendNotification(car.Name);
                    SaveChanges();
                }
            }
        }

        private async Task SendNotification(string carName)
        {
            var apiKey = _config.GetValue<string>("SENDGRID_API_KEY");
            var emailFrom = _config.GetValue<string>("SENDGRID_EMAIL_FROM");
            var emailTo = _config.GetValue<string>("SENDGRID_EMAIL_TO");

            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(emailFrom),
                Subject = "Reminder",
                PlainTextContent = $"Hello, your contract for {carName} is about to expire!",
                HtmlContent = $"<strong>Hello, your contract for {carName} is about to expire!</strong>"
            };
            msg.AddTo(new EmailAddress(emailTo));
            await client.SendEmailAsync(msg);
        }

        private void SaveChanges()
        {
            _rentedCarsContext.SaveChangesAsync();
            _carsContext.SaveChangesAsync();
        }
    }
}