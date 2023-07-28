namespace FarmFresh.Api.DTOs
{
    public class EnvironmentDto
    {
        public bool Day { get; set; }
        public bool Windy { get; set; }
        public bool Sunny { get; set; }
        public double PowerPrice { get; set; }
        public required List<double> SellPrices { get; set; }
    }
}