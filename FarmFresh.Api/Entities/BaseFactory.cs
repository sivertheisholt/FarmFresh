namespace FarmFresh.Api.Entities
{
    public class BaseFactory
    {
        public int FactoryId { get; set; }
        public bool Active { get; set; } = false;
        public int Production { get; set; }
        public double PowerUsage { get; set; }
    }
}