

namespace Cars.Models.Requests
{
    public class CarRequest
    {
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string CustomerId { get; set; }
    }
}
