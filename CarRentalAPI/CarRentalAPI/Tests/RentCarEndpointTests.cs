using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;

namespace CarRentalAPI.Tests
{
    public class RentCarEndpointTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly IConfiguration _config;
        private const string _baseUrl = "https://localhost:44319/";


        public RentCarEndpointTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
            _config = new ConfigurationRoot(new List<IConfigurationProvider>());
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        [InlineData(4, 4)]
        [InlineData(10, 5)]
        [InlineData(14, 6)]
        [InlineData(6, 7)]
        [InlineData(12, 8)]
        [InlineData(5, 9)]
        [InlineData(7, 10)]
        public async Task WhenRentCarEnpointIsCalled_AndCarIsAvailableToRent_ThenReturnSuccessfulMessage(int Id, int rentPeriod)
        {
            // Act 
            var response = await _client.PutAsync($"https://localhost:44319/api/Car/rent/{Id}/{rentPeriod}", null);

            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<string>(responseString);

            // Assert
            Assert.Equal("Car successfully rented", actual);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        [InlineData(1, 11)]
        [InlineData(1, 20)]
        public async Task WhenRentCarEnpointIsCalled_AndRentPeriodIsOutsideOfAllowedRange_ThenReturnBadRequest(int Id, int rentPeriod)
        {
            // Act 
            var response = await _client.PutAsync($"https://localhost:44319/api/Car/rent/{Id}/{rentPeriod}", null);

            // Assert
            Assert.Equal("Bad Request", response.ReasonPhrase);
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