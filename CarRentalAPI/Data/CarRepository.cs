using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalAPI.Interfaces;
using CarRentalAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CarRentalAPI.Data
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
            return await _carsContext.Cars.ToListAsync();
        }

        public async Task<IEnumerable<RentedCar>> GetRentedCars()
        {
            return await _rentedCarsContext.RentedCars.ToListAsync();
        }

        public async Task<string> RentCar(int id, int rentPeriod)
        {
            var car = await _carsContext.Cars.FirstOrDefaultAsync(c => c.Id == id);
            var ts = new TimeSpan(0, rentPeriod, 0);

            if (car is not null && car.Stock is not 0)
            {
                car.Stock -= 1;
                if (car.Stock == 0)
                {
                    car.Status = "Unavailable";
                }

                // check if ID is already in db, if yes then increment
                while (await _rentedCarsContext.RentedCars.AnyAsync(c => c.Id == id))
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

                await _rentedCarsContext.RentedCars.AddAsync(rentCar);
            }
            else
            {
                return "Car not in stock";
            }
            await SaveChanges();
            return "Car successfully rented";
        }
        
        public async Task<string> ReturnCar(int id)
        {
            var rentedCar = await _rentedCarsContext.RentedCars.FirstOrDefaultAsync(c => c.Id == id);

            if (rentedCar is not null)
            {
                var car = await _carsContext.Cars.FirstOrDefaultAsync(c => c.Name == rentedCar.Name);
                car.Stock += 1;
                car.Status = "Available";
                _rentedCarsContext.RentedCars.Remove(rentedCar);
            }
            else
            {
                return "No cars to return";
            }

            await SaveChanges();
            return "Car successfully returned";
        }
        
        

        public async Task CheckExpiry()
        {
            var ts = new TimeSpan(0, 01, 0);
            var rentedCars = await _rentedCarsContext.RentedCars.ToListAsync();
            
            foreach (var car in rentedCars)
            {
                var aboutToExpire = car.ExpiryDate - ts;
                await SendNotification(car.Name);

                if (car.ExpiryDate <= DateTime.Now)
                {
                    car.Status = "Expired";
                    await SaveChanges();
                }
                else if (DateTime.Now >= aboutToExpire &&
                         DateTime.Now < car.ExpiryDate &&
                         car.NotificationSent is false)
                {
                    car.NotificationSent = true;
                    await SendNotification(car.Name);
                    await SaveChanges();
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

        private async Task SaveChanges()
        {
            await _rentedCarsContext.SaveChangesAsync();
            await _carsContext.SaveChangesAsync();
        }
    }
}