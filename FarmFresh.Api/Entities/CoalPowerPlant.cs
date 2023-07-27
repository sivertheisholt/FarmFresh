namespace FarmFresh.Api.Entities
{
    public class CoalPowerPlant
    {
        public int PlantId { get; set; }
        public double Production { get; set; } = 130;
        public bool Active { get; set; }
        public double Price { get; set; }
    }
}