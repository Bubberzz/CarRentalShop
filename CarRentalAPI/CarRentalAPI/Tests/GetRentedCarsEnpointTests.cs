using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CarRentalAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace CarRentalAPI.Tests
{
    public class GetRentedCarsEnpointTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public GetRentedCarsEnpointTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task WhenGetRentedCarsEnpointIsCalled_AndNoCarsAreRented_ThenReturnMessage()
        {
            // Act
            var response = await _client.GetAsync("https://localhost:44319/api/Car/rented");
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal("[]", responseString);
      
        }
    }
}