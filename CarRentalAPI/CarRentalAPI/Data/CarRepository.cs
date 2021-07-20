using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalAPI.Interfaces;
using CarRentalAPI.Models;


namespace CarRental.Data
{
    public class CarRepository : ICarRepository
    {
  
        public Task<IEnumerable<Car>> GetCars()
        {
           
        }

        public Task<IEnumerable<RentedCar>> GetRentedCars()
        {
            
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