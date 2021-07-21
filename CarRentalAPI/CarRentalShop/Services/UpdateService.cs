using System.Net.Http;
using System.Threading;
using System.Timers;

namespace CarRentalShop.Services
{
    public class UpdateService
    {
        private readonly HttpClient _client = new();
        private CarRentalAPIClient apiClient;
        public UpdateService()
        {
            var timer = new System.Timers.Timer();
            timer.Elapsed += CheckRentedCarExpiry;
            timer.Interval = 30000;
            timer.Enabled = true;
            _client.DefaultRequestHeaders.Add("ApiKey", "sdf324SdGgD4324rGdfHG3FDghF45TgD2hgDRGdr");
        }

        private async void CheckRentedCarExpiry(object source, ElapsedEventArgs e)
        {
            apiClient = new CarRentalAPIClient( "https://localhost:44319/", _client);
            await apiClient.CheckAsync(CancellationToken.None);
        }
    }
}