using FarmFresh.Api.Entities;

namespace FarmFresh.Api
{
    public class SimulationEnvironment
    {
        public bool Day { get; set; } = true;
        public bool Windy { get; set; }
        public bool Sunny { get; set; }
        public Solar Solar { get; set; }
        public Wind Wind { get; set; }
        public double PowerPrice { get; set; } // nok/kWh
        public List<double> SellPrices { get; set; } // Organic Fertilizer factory, Organic Seeds Factory, Pest And Dieseases Factory, Soil Amendments Factory
        public SimulationEnvironment()
        {
            Random random = new();

            Solar = new Solar();
            Wind = new Wind();
            SellPrices = new List<double>()
            {
                random.NextDouble() * (500.0 - 100) + 100,
                random.NextDouble() * (500.0 - 100) + 100,
                random.NextDouble() * (500.0 - 100) + 100,
                random.NextDouble() * (500.0 - 100) + 100
            };
        }
    }
}