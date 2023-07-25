using FarmFresh.Api.Entities;

namespace FarmFresh.Api
{
    public class SimulationEnvironment
    {
        public bool Day { get; set; }
        public bool Windy { get; set; }
        public bool Sunny { get; set; }
        public Solar Solar { get; set; }
        public Wind Wind { get; set; }
        public List<CoalFactory> CoalFactories { get; set; }
        public SimulationEnvironment()
        {
            Solar = new Solar
            {
                Production = 0
            };
            Wind = new Wind
            {
                Production = 0
            };
            CoalFactories = new List<CoalFactory>()
            {
                new CoalFactory
                {
                    Active = false,
                    Production = 0
                },
                new CoalFactory
                {
                    Active = false,
                    Production = 0
                },
                new CoalFactory
                {
                    Active = false,
                    Production = 0
                },
                new CoalFactory
                {
                    Active = false,
                    Production = 0
                },
            };
        }
    }
}