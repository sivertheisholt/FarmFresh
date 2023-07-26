using FarmFresh.Api.Data;
using FarmFresh.Api.Entities;
using FarmFresh.Api.Interfaces.IRepositories;

namespace FarmFresh.Api.Repositories
{
    public class SoilAmendmentsFactoryRepository : BaseRepository<SoilAmendmentsFactory>, ISoilAmendmentsFactoryRepository
    {
        public SoilAmendmentsFactoryRepository(DataContext context) : base(context)
        {
        }
    }
}