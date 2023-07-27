namespace FarmFresh.Api.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public required string UserUUID { get; set; }
        public required string GroupName { get; set; }
        public double Balance { get; set; }
        public required List<CoalPowerPlant> CoalPowerPlants { get; set; }
        public required OrganicFertilizerFactory OrganicFertilizerFactory { get; set; }
        public required OrganicSeedsFactory OrganicSeedsFactory { get; set; }
        public required PestAndDiseaseFactory PestAndDiseaseFactory { get; set; }
        public required SoilAmendmentsFactory SoilAmendmentsFactory { get; set; }
    }
}