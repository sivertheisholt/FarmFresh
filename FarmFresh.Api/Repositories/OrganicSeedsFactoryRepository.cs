using FarmFresh.Api.Data;
using FarmFresh.Api.Entities;
using FarmFresh.Api.Interfaces.IRepositories;

namespace FarmFresh.Api.Repositories
{
    public class OrganicSeedsFactoryRepository : BaseRepository<OrganicSeedsFactory>, IOrganicSeedsFactoryRepository
    {
        public OrganicSeedsFactoryRepository(DataContext context) : base(context)
        {
        }
    }
}