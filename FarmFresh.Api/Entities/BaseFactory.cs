namespace FarmFresh.Api.Entities
{
    public class BaseFactory
    {
        public bool Active { get; set; } = false;
        public int Production { get; set; }
        public double PowerUsage { get; set; }
    }
}