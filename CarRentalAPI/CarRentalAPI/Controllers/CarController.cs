using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalAPI.Interfaces;
using CarRentalAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _repository;

        public CarController(ICarRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            var cars = await _repository.GetCars();
            return Ok(cars);
        }

        [Route("rented")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentedCar>>> GetRentedCars()
        {
            var cars = await _repository.GetRentedCars();
            return Ok(cars);
        }
        
        [Route("rented/check")]
        [HttpGet]
        public async Task<ActionResult<string>> CheckRentedCarExpiry()
        {
        
        }
        
        
        [HttpPut("rent/{id}/{rentPeriod}")]
        public async Task<ActionResult<string>> RentCar(int id, int rentPeriod)
        {
        
        }
        
        [HttpPut("return/{id}")]
        public async Task<ActionResult<string>> ReturnCar(int id)
        {
        
        }
    }
}
