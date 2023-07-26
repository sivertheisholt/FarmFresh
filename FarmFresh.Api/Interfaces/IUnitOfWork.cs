using FarmFresh.Api.Interfaces.IRepositories;

namespace FarmFresh.Api.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ICoalPowerPlantRepository CoalPowerPlantRepository { get; }
        IOrganicFertilizerFactoryRepository OrganicFertilizerFactoryRepository { get; }
        IOrganicSeedsFactoryRepository OrganicSeedsFactoryRepository { get; }
        IPestAndDiseaseFactoryRepository PestAndDiseaseFactoryRepository { get; }
        ISoilAmendmentsFactoryRepository SoilAmendmentsFactoryRepository { get; }
        Task<bool> Complete();
        bool HasChanged();
    }
}