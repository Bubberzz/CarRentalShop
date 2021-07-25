using System;

namespace CarRentalAPI.Models
{
    public class RentedCar : Car
    {
        public DateTime ExpiryDate { get; set; }
        public bool NotificationSent { get; set; } = false;
    }
}