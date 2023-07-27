namespace FarmFresh.Api.Entities
{
    public class PestAndDiseaseFactory
    {
        public int FactoryId { get; set; }
        public bool Active { get; set; } = false;
        public int Production { get; set; } // Per hour
        public double PowerUsage { get; set; } // kWh
        public double Capacity { get; set; } // Current capacity
        public double MaxCapacity { get; set; } // Max capacity
        public bool Selling { get; set; } = false;
        public PestAndDiseaseFactory()
        {
            Production = 1;
            PowerUsage = 220.0;
            Capacity = 0.0;
            MaxCapacity = 10.0;
        }
    }
}