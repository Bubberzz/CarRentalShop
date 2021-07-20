using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace CarRentalAPI.Tests
{
    public class RentCarEndpointTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public RentCarEndpointTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task WhenRentCarEnpointIsCalled_AndCarIsAvailableToRent_ThenReturnSuccessfulMessage()
        {
            // Act
            var response = await _client.PutAsync("https://localhost:44319/api/Car/rent/1/2", null);
            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<string>(responseString);

            // Assert
            Assert.Equal("Car successfully rented", actual);
        }

        [Fact]
        public async Task WhenRentCarEnpointIsCalled_AndCarIsNotAvailableToRent_ThenReturnUnsuccessfulMessage()
        {
            // Act
            await _client.PutAsync("https://localhost:44319/api/Car/rent/1/2", null);
            var response = await _client.PutAsync("https://localhost:44319/api/Car/rent/1/2", null);
            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<string>(responseString);

            // Assert
            Assert.Equal("Car not in stock", actual);
        }
    }
}