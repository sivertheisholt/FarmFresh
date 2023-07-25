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
        public List<CoalPowerPlant> CoalPowerPlants { get; set; }
        public List<BaseFactory> Factories { get; set; }
        public SimulationEnvironment()
        {
            Solar = new Solar();
            Wind = new Wind();
            CoalPowerPlants = new List<CoalPowerPlant>()
            {
                new CoalPowerPlant(),
                new CoalPowerPlant(),
                new CoalPowerPlant(),
                new CoalPowerPlant()
            };
            Factories = new List<BaseFactory>()
            {
                new OrganicFertilizerFactory(),
                new OrganicSeedsFactory(),
                new PestAndDiseaseFactory(),
                new SoilAmendmentsFactory()
            };
        }
    }
}