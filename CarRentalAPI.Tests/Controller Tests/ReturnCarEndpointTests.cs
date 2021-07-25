using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace CarRentalAPI.Tests
{
    public class ReturnCarEndpointTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;


        public ReturnCarEndpointTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task WhenReturnCarEndpointIsCalled_AndThereIsCarsToReturn_ThenReturnSuccessfulMessage(int Id)
        {
            // Act 
            await _client.PutAsync($"https://localhost:44319/api/Car/rent/{Id}/3", null);
            var response = await _client.PutAsync($"https://localhost:44319/api/Car/return/{Id}", null);

            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<string>(responseString);

            // Assert
            Assert.Equal("Car successfully returned", actual);
        }

        [Fact]
        public async Task WhenReturnCarEndpointIsCalled_AndThereIsNoCarsToReturn_ThenReturnUnsuccessfulMessage()
        {
            // Act 
            var response = await _client.PutAsync($"https://localhost:44319/api/Car/return/3", null);

            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<string>(responseString);

            // Assert
            Assert.Equal("No cars to return", actual);
        }
    }
}