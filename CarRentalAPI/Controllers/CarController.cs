using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalAPI.Attributes;
using CarRentalAPI.Interfaces;
using CarRentalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarRentalAPI.Controllers
{
    [ApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _repository;

        public CarController(ICarRepository repository)
        {
            _repository = repository;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            var cars = await _repository.GetCars();
            return Ok(cars);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("rented")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentedCar>>> GetRentedCars()
        {
            var cars = await _repository.GetRentedCars();
            return Ok(cars);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("rented/check")]
        [HttpGet]
        public async Task<ActionResult<string>> CheckRentedCarExpiry()
        {
            await _repository.CheckExpiry();
            return Ok(JsonConvert.SerializeObject("Expiry check successful"));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("rent/{id}/{rentPeriod}")]
        public async Task<ActionResult<string>> RentCar(int id, int rentPeriod)
        {
            if (rentPeriod is < 2 or > 10)
            {
                return BadRequest("Please select between 2 and 10");
            }
            var response = await _repository.RentCar(id, rentPeriod);
            return Ok(JsonConvert.SerializeObject(response));
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("return/{id}")]
        public async Task<ActionResult<string>> ReturnCar(int id)
        {
            var response = await _repository.ReturnCar(id);
            return Ok(JsonConvert.SerializeObject(response));
        }
    }
}
