namespace FarmFresh.Api.DTOs
{
    public class FactoryDto
    {
        public bool Active { get; set; } = false;
        public double Capacity { get; set; } // Current capacity
        public double MaxCapacity { get; set; } // Max capacity
        public bool Selling { get; set; } = false;
    }
}