using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;
using CarRentalAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;

namespace CarRentalAPI.Tests
{
    public class GetCarsEndpointTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public GetCarsEndpointTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task WhenGetCarsEnpointIsCalled_ThenReturnCarList()
        {
            // Act
            var response = await _client.GetAsync("https://localhost:44319/api/Car");
            var responseString = await response.Content.ReadAsStringAsync();
            var cars = JsonConvert.DeserializeObject<List<Car>>(responseString);

            // Assert
            Assert.Equal(15, cars.Count);
            Assert.Equal(1, cars[0].Id);
            Assert.Equal("Audi R8", cars[0].Name);
            Assert.Equal(15, cars[14].Id);
            Assert.Equal("Volvo XC90", cars[14].Name);
        }
    }
}