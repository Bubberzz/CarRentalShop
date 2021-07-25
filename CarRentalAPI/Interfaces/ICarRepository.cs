using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalAPI.Models;

namespace CarRentalAPI.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetCars();
        Task<IEnumerable<RentedCar>> GetRentedCars();
        Task<string> RentCar(int id, int rentPeriod);
        Task<string> ReturnCar(int id);
        Task CheckExpiry();
    }
}