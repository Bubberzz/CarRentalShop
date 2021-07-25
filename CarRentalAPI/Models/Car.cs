namespace CarRentalAPI.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}