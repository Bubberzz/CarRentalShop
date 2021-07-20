using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
      
        }

        [Route("rented")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentedCar>>> GetRentedCars()
        {

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
