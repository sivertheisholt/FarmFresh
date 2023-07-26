using FarmFresh.Api.Data;
using FarmFresh.Api.Entities;
using FarmFresh.Api.Interfaces.IRepositories;

namespace FarmFresh.Api.Repositories
{
    public class OrganicFertilizerFactoryRepository : BaseRepository<OrganicFertilizerFactory>, IOrganicFertilizerFactoryRepository
    {
        public OrganicFertilizerFactoryRepository(DataContext context) : base(context)
        {
        }
    }
}